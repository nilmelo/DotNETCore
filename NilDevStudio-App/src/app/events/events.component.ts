import { Component, OnInit, TemplateRef } from '@angular/core';
import { MyEventService } from '../services/MyEvent.service';
import { MyEvent } from '../models/MyEvent';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit
{
  FilteredEvents: MyEvent[];
  events: MyEvent[];
  imageWidth = 50;
  imageMargin = 2;
  imageToggle = false;
  modalRef: BsModalRef;
  registerForm: FormGroup;

  _filterList: string;

  constructor(
        private myEventService: MyEventService
      , private modalService: BsModalService
      , private fb: FormBuilder
    ) { }

  openModal(template: TemplateRef<any>)
  {
      this.modalRef = this.modalService.show(template);
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

  saveChanges()
  {

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

}
