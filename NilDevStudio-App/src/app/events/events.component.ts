import { Component, OnInit, TemplateRef } from '@angular/core';
import { MyEventService } from '../services/MyEvent.service';
import { MyEvent } from '../models/MyEvent';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
// older Angular versions
import {CommonModule} from '@angular/common';



@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit
{
  FilteredEvents: any[];
  events: MyEvent[];
  imageWidth = 50;
  imageMargin = 2;
  imageToggle = false;
  modalRef: BsModalRef;

  _filterList: string;

  constructor(
        private myEventService: MyEventService
      , private modalService: BsModalService
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
