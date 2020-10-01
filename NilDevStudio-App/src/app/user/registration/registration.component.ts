import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/User';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
	selector: 'app-registration',
	templateUrl: './registration.component.html',
	styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit
{
	registerForm: FormGroup;
	user: User;

	constructor(
		private authService: AuthService,
		public router: Router,
		private fb: FormBuilder,
		private toastr: ToastrService){}

	ngOnInit(){}

	validation()
	{
		this.registerForm = this.fb.group({
			fullName: ['', Validators.required],
			email: ['',[Validators.required, Validators.email]],
			userName: ['', Validators.required],
			passwords: this.fb.group({
				password: ['', [Validators.required, Validators.minLength(4)]],
				confirmPassword: ['', Validators.required]
			}, { validator: this.pwCompare })
		});
	}

	pwCompare(fb: FormGroup)
	{
		const pwConfirm = fb.get('confirmPassword');

		if(pwConfirm.errors == null || 'mismatch' in pwConfirm.errors)
		{
			if(fb.get('password').value !== pwConfirm.value)
			{
				pwConfirm.setErrors({ mismatch: true });
			}
			else
			{
				pwConfirm.setErrors(null);
			}
		}
	}

	registerUser()
	{
		if(this.registerForm.valid)
		{
			this.user = Object.assign(
				{password: this.registerForm.get('passwords.password').value},
				this.registerForm.value);

			this.authService.register(this.user).subscribe(
				() => {
					this.router.navigate(['/user/login']);
					this.toastr.success('Cadastro Realizado');
				},
				error => {
					const err = error.error;
					err.forEach(element => {
						switch(element.code) {
							case 'DuplicatedUserName':
								this.toastr.error('Account already exist');
								break;
							default:
								this.toastr.error(`Error: ${element.code}`);
								break;
						}
					});
				}
			)
		}
	}

}
