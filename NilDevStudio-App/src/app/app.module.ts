import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { MyEventService } from './services/MyEvent.service';
import { AppComponent } from './app.component';
import { EventsComponent } from './events/events.component';
import { MyEventEditComponent } from './events/myEventEdit/myEventEdit.component';
import { NavComponent } from './nav/nav.component';
import { DateTimeFormatPipePipe } from './helper/DateTimeFormatPipe.pipe';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { NgxMaskModule } from 'ngx-mask';
import { NgxCurrencyModule } from 'ngx-currency';
import { SpeakersComponent } from './speakers/speakers.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ContactComponent } from './contact/contact.component';
import { TitleComponent } from './shared/title/title.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { AuthInterceptor } from './auth/interceptor';

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
	  RegistrationComponent,
	  MyEventEditComponent
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
	TabsModule.forRoot(),
	NgxCurrencyModule,
    ReactiveFormsModule,
	BrowserAnimationsModule,
	NgxMaskModule.forRoot(),
	ToastrModule.forRoot({
		timeOut: 3000,
		preventDuplicates: true
	})
  ],
  providers: [
	  {
		  provide: HTTP_INTERCEPTORS,
		  useClass: AuthInterceptor,
		  multi: true
	  }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
