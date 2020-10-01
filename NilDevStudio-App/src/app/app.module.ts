import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { MyEventService } from './services/MyEvent.service';
import { AppComponent } from './app.component';
import { EventsComponent } from './events/events.component';
import { NavComponent } from './nav/nav.component';
import { DateTimeFormatPipePipe } from './helper/DateTimeFormatPipe.pipe';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { SpeakersComponent } from './speakers/speakers.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ContactComponent } from './contact/contact.component';
import { TitleComponent } from './shared/title/title.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
      EventsComponent,
      NavComponent,
      DateTimeFormatPipePipe,
      SpeakersComponent,
      DashboardComponent,
	  ContactComponent,
	  TitleComponent,
	  UserComponent,
	  LoginComponent,
	  RegistrationComponent
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    TooltipModule.forRoot(),
    ReactiveFormsModule,
	BrowserAnimationsModule,
	ToastrModule.forRoot({
		timeOut: 3000,
		preventDuplicates: true
	})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
