import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
	selector: 'app-nav',
	templateUrl: './nav.component.html',
	styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit
{

	constructor(
		private authService: AuthService,
		public router: Router) {}

	ngOnInit() {}

	showMenu()
	{
		return this.router.url !== '/user/login';
	}

	showMenu()
	{
		return this.router.url !== '/user/login';
	}

	loggedIn()
	{
		return this.authService.loggedIn();
	}

	enter()
	{
		this.router.navigate(['/user/login']);
	}

	logout()
	{
		localStorage.removeItem('token');
		this.router.navigate(['/user/login']);
	}

	userName()
	{
		return sessionStorage.getItem('username');
	}

}
