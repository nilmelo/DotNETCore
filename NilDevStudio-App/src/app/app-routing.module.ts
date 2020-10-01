import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EventsComponent } from './events/events.component';
import { SpeakersComponent } from './speakers/speakers.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ContactComponent } from './contact/contact.component';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';


const routes: Routes = [
	{
		path: 'user', component: UserComponent,
		children: [
			{ path: 'login', component: LoginComponent },
			{ path: 'registration', component: RegistrationComponent }
		]
	},

	{ path: 'events', component: EventsComponent },
	{ path: 'speakers', component: SpeakersComponent },
	{ path: 'dashboard', component: DashboardComponent },
	{ path: 'contact', component: ContactComponent },
	{ path: '', redirectTo: 'dashboard', pathMatch: 'full' },
	{ path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
