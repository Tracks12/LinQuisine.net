import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.css']
})

export class LoginComponent {
		addressForm = this.fb.group({
		company: null,
		firstName: [null, Validators.required],
		lastName: [null, Validators.required],
		address: [null, Validators.required],
		address2: null,
		city: [null, Validators.required],
		state: [null, Validators.required],
		postalCode: [null, Validators.compose([
			Validators.required, Validators.minLength(5), Validators.maxLength(5)])
		],
		shipping: ['free', Validators.required]
	});

	hasUnitNumber = false;

	states = [{ name: 'Alabama', abbreviation: 'AL' }];

	constructor(private fb: FormBuilder) {}

	onSubmit(): void {
		alert('Thanks!');
	}
}
