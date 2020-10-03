import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MyEvent } from '../models/MyEvent';

// Decorator
@Injectable({
  providedIn: 'root'
})
export class MyEventService
{
	baseURL = 'http://localhost:5000/api/myEvent';

    constructor(private http: HttpClient) {}

    getAllMyEvent(): Observable<MyEvent[]>
    {
		return this.http.get<MyEvent[]>(this.baseURL);
    }

    getMyEventByTheme(theme: string): Observable<MyEvent[]>
    {
        return this.http.get<MyEvent[]>(`${this.baseURL}/getByTheme/${theme}`);
    }

    getMyEventById(id: number): Observable<MyEvent>
    {
        return this.http.get<MyEvent>(`${this.baseURL}/${id}`);
	}

	postUpload(file: File, name: string)
	{
		const fileToUpload = <File>file[0];
		const formData = new FormData();
		formData.append('file', fileToUpload, name);

		return this.http.post(`${this.baseURL}/upload`, formData);
	}

	postMyEvent(myEvent: MyEvent)
	{
		return this.http.post(this.baseURL, myEvent);
	}

	putMyEvent(myEvent: MyEvent)
	{
		return this.http.put(`${this.baseURL}/${myEvent.id}`, myEvent);
    }

    deleteMyEvent(id: number)
    {
        return this.http.delete(`${this.baseURL}/${id}`);
    }

}
