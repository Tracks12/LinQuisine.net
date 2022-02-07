import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';

import { ApiService } from '../_commons/services/api.service';
import { Recipe } from '../_commons/models/recipe';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],

})

export class DashboardComponent implements OnInit {
	foods: Recipe[] = [];

	constructor(
		private apiService: ApiService,
		private route: ActivatedRoute,
		public dialog: MatDialog
	) { }

	ngOnInit(): void {
		this.apiService.getAllRecipes().subscribe(r => {
			//mettre r après le =
			this.foods = [];
		});

		this.route.params.subscribe(params => {
			if(params['searchTerm']) {
				this.foods = this.foods.filter(r => r.name.toLowerCase().includes(params['searchTerm'].toLowerCase()));
			}
		});
	}
}
