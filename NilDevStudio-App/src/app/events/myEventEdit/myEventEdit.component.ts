import { Component, OnInit } from '@angular/core';
import { MyEventService } from 'src/app/services/MyEvent.service';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { MyEvent } from 'src/app/models/MyEvent';
import { ActivatedRoute } from '@angular/router';

@Component({
	selector: 'app-myevent-edit',
	templateUrl: './myEventEdit.component.html',
	styleUrls: ['./myEventEdit.component.css']
})
export class MyEventEditComponent implements OnInit
{
	title = 'Edit Event';
	event: MyEvent = new MyEvent();
	imageURL = 'assets/img/upload.png';
	registerForm: FormGroup;
	file: File;
	fileNameToUpdate: string;
	dateNow = '';
	dateEvent = ''; // TODO: Revisar uso

	get lots(): FormArray
	{
		return this.registerForm.get('lots') as FormArray;
	}

	get socialNetworks(): FormArray
	{
		return this.registerForm.get('socialNetworks') as FormArray;
	}

	constructor(
		private myEventService: MyEventService
	  , private fb: FormBuilder
	  , private localeService: BsLocaleService
	  , private toastr: ToastrService
	  , private router: ActivatedRoute
	  ) { this.localeService.use('pt-br'); }

	ngOnInit()
	{
		this.validation();
		this.loadMyEvent();
	}

	loadMyEvent()
	{
		const idEvent = +this.router.snapshot.paramMap.get('id');
		this.myEventService.getMyEventById(idEvent).subscribe(
			(myEvent: MyEvent) => {
				this.event = Object.assign({}, myEvent);
				this.fileNameToUpdate = myEvent.imageURL.toString();
				this.imageURL = `http://localhost:5000/resources/images/${this.event.imageURL}?_pog=${this.dateNow}`;
				this.event.imageURL = '';
				this.registerForm.patchValue(this.event);

				this.event.lots.forEach(lot => {
					this.lots.push(this.createLot(lot));
				});

				this.event.socialNetworks.forEach(socialNetwork => {
					this.socialNetworks.push(this.createSocial(socialNetwork));
				});

			}
		)
	}

	validation()
	{
		this.registerForm = this.fb.group({
			id: [],
			theme: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
			local: ['', Validators.required],
			dateEvent: ['', Validators.required],
			imageURL: [''],
			quantPeople: ['', [Validators.required, Validators.max(2000)]],
			telephone: ['', Validators.required],
			email: ['', [Validators.required, Validators.email]],
			lots: this.fb.array([]),
			socialNetworks: this.fb.array([])
		});
	}

	createLot(lot: any): FormGroup
	{
		return this.fb.group({
			id: [lot.id],
			name: [lot.name, Validators.required],
			quantity: [lot.quantity, Validators.required],
			price: [lot.price, Validators.required],
			dateStart: [lot.dateStart],
			dateEnd: [lot.dateEnd]
		});
	}

	createSocial(socialNetwork: any): FormGroup
	{
		return this.fb.group({
			id: [socialNetwork.id],
			name: [socialNetwork.name, Validators.required],
			url: [socialNetwork.url, Validators.required]
		});
	}

	addLot()
	{
		this.lots.push(this.createLot({ id: 0 }));
	}

	addSocial()
	{
		this.socialNetworks.push(this.createSocial({ id: 0 }));
	}

	removeLot(id: number)
	{
		this.lots.removeAt(id);
	}

	removeSocial(id: number)
	{
		this.socialNetworks.removeAt(id);
	}

	onFileChange(event: any, file: FileList)
	{
		const reader = new FileReader();
		reader.onload = (event: any) => this.imageURL = event.target.result;

		this.file = event.target.files;
		reader.readAsDataURL(file[0]);
	}

	saveEvent()
	{
		this.event = Object.assign({id: this.event.id}, this.registerForm.value);
		this.event.imageURL = this.fileNameToUpdate;

		this.uploadImage();

		this.myEventService.putMyEvent(this.event).subscribe(
			() => {
				this.toastr.success('Successfully Edited!');
			}, error => {
				this.toastr.error('Edit Error');
			}
		);
	}

	uploadImage()
	{
		if(this.registerForm.get('imageURL').value !== '')
		{
			this.myEventService.postUpload(this.file, this.fileNameToUpdate)
			.subscribe(
				() => {
					this.dateNow = new Date().getMilliseconds().toString();
					this.imageURL = `http://localhost:5000/resources/images/${this.event.imageURL}?_pog=${this.dateNow}`;
				}
			);
		}

	}

}
