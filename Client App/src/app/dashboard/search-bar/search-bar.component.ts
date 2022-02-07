import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ApiService } from '../../_commons/services/api.service';

@Component({
	selector: 'app-search-bar',
	templateUrl: './search-bar.component.html',
	styleUrls: ['./search-bar.component.css']
})

export class SearchBarComponent implements OnInit {
	public searchTerm: string = "";

	constructor(
		private dataService: ApiService,
		private route: ActivatedRoute,
		private router: Router
	) { }

	ngOnInit() {
		this.route.params.subscribe(params => {
			if (params['searchTerm'])
				this.searchTerm = params['searchTerm'];
		})
	}

	search(): void{
		if(this.searchTerm)
			this.router.navigateByUrl('/search/' + this.searchTerm);
	}
}
