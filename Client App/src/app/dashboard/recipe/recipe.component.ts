import { Component, Input, OnInit } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';

import { ApiService } from '../../_commons/services/api.service';
import { Recipe } from '../../_commons/models/recipe';

@Component({
	selector: 'app-recipe',
	templateUrl: './recipe.component.html',
	styleUrls: ['./recipe.component.css'],
	animations: [
		trigger('bodyExpansion', [
			state('collapsed, void', style({ height: '0px', visibility: 'hidden' })),
			state('expanded', style({ height: '*', visibility: 'visible' })),
			transition('expanded <=> collapsed, void => collapsed',
			animate('350ms cubic-bezier(0.2, 0.0, 0.3, 1)')),
		])
	]
})

export class RecipeComponent implements OnInit {
	ing: string[] = ['Patates', 'Poireaux', 'Tomates', 'Abricot', 'Ver de terre'];
	status: any;
	state: string = 'collapsed';

	@Input() recipe!: Recipe;

	constructor(
		private apiService: ApiService
	) { }

	ngOnInit(): void {
		this.apiService.getStatus().subscribe((data) => this.status = data['online']);
	}

	toggle(): void {
		this.state = this.state === 'collapsed' ? 'expanded' : 'collapsed';
	}
}
