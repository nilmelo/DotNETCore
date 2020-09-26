import { Component, OnInit } from '@angular/core';
import { MyEventService } from '../_services/MyEvent.service';
// older Angular versions
import {CommonModule} from '@angular/common';
import { MyEvent } from '../_models/MyEvent';


@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit
{
  _filterList: string;
  get filterList(): string
  {
    return this._filterList;
  }
  set filterList(value: string)
  {
    this._filterList = value;
    this.FilteredEvents = this.filterList ? this.FilterEvents(this.filterList) : this.events;
  }

  FilteredEvents: MyEvent[];
  events: MyEvent[];
  imageWidth = 50;
  imageMargin = 2;
  imageToggle = false;

  constructor(private myEventService: MyEventService) { }

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
