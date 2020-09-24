import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
// older Angular versions
import {CommonModule} from '@angular/common';

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

  FilteredEvents: any = [];
  events: any = [];
  imageWidth = 50;
  imageMargin = 2;
  imageToggle = false;

  constructor(private http: HttpClient) { }

  ngOnInit()
  {
    this.getEvents();
  }

  ToggleImage()
  {
    this.imageToggle = !this.imageToggle;
  }

  FilterEvents(filterBy: string): any
  {
    filterBy = filterBy.toLocaleLowerCase();
    return this.events.filter(
      event => event.theme.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  getEvents()
  {
    this.http.get('http://localhost:5000/api/values').subscribe(
      response => {
        this.events = response;
      },
      error => {
        console.log(error);
      }
    );
  }

}
