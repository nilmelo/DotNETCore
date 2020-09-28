import { Component, OnInit, TemplateRef } from '@angular/core';
import { MyEventService } from '../services/MyEvent.service';
import { MyEvent } from '../models/MyEvent';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';

defineLocale('pt-br', ptBrLocale);

@Component({selector: 'app-events',
	templateUrl: './events.component.html',
	styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit
{
	FilteredEvents: MyEvent[];
	events: MyEvent[];
	myEvent: MyEvent;
	imageWidth = 50;
	imageMargin = 2;
	imageToggle = false;
	registerForm: FormGroup;
	saveMod = 'post';
	bodyDeleteEvent = '';

	_filterList: string;

	constructor(
		private myEventService: MyEventService
		, private modalService: BsModalService
		, private fb: FormBuilder
		, private localeService: BsLocaleService
		) { this.localeService.use('pt-br'); }

	editEvent(myEvent: MyEvent, template: any)
	{
		this.saveMod = 'put';
		this.openModal(template);
		this.myEvent = myEvent;
		this.registerForm.patchValue(myEvent);
	}

	newEvent(template: any)
	{
		this.saveMod = 'post';
		this.openModal(template);
	}

	openModal(template: any)
	{
		this.registerForm.reset();
		template.show();
	}
	get filterList(): string
	{
		return this._filterList;
	}
	set filterList(value: string)
	{
		this._filterList = value;
		this.FilteredEvents = this.filterList ? this.FilterEvents(this.filterList) : this.events;
	}

	ngOnInit()
	{
		this.validation();
		this.getEvents();
	}

	ToggleImage()
	{
		this.imageToggle = !this.imageToggle;
	}

	FilterEvents(filterBy: string): MyEvent[]
	{
		filterBy = filterBy.toLocaleLowerCase();
		return this.events.filter(
			event => event.theme.toLocaleLowerCase().indexOf(filterBy) !== -1
			);
	}

	validation()
	{
		this.registerForm = this.fb.group({
			theme: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
			local: ['', Validators.required],
			dateEvent: ['', Validators.required],
			imageURL: ['', Validators.required],
			quantPeople: ['', [Validators.required, Validators.max(2000)]],
			telephone: ['', Validators.required],
			email: ['', [Validators.required, Validators.email]]
		});
	}

	saveChanges(template: any)
	{
		if(this.registerForm.valid)
		{
			if(this.saveMod === 'post')
			{
				this.myEvent = Object.assign({}, this.registerForm.value);
				this.myEventService.postMyEvent(this.myEvent).subscribe(
					(newEvent: MyEvent) => {
						console.log(newEvent);
						template.hide();
						this.getEvents();
					}, error => {
						console.log(error);
					}
				);
			}
			else
			{
				this.myEvent = Object.assign({id: this.myEvent.id}, this.registerForm.value);
				this.myEventService.putMyEvent(this.myEvent).subscribe(
					() => {
						template.hide();
						this.getEvents();
					}, error => {
						console.log(error);
					}
				);
			}
		}
	}

	getEvents()
	{
		this.myEventService.getAllMyEvent().subscribe(
			(_myEvents: MyEvent[]) => {
				this.events = _myEvents;
				this.FilteredEvents = this.events;

				console.log(_myEvents);
			},
			error => {
				console.log(error);
			}
			);
	}

	excludeEvent(myEvent: MyEvent, template: any) {
		this.openModal(template);
		this.myEvent = myEvent;
		this.bodyDeleteEvent = `Tem certeza que deseja excluir o Evento: ${myEvent.theme}, Código: ${myEvent.id}`;
	}

	confirmDelete(template: any)
	{
		this.myEventService.deleteMyEvent(this.myEvent.id).subscribe(
			() => {
				template.hide();
				this.getEvents();
			  }, error => {
				console.log(error);
			  }
		);
	}

}
