import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { ApiService } from './../api.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-search-bar',
    templateUrl: './search-bar.component.html',
    styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  searchTerm:String = "";
    constructor(
        private dataService: ApiService,
        private route: ActivatedRoute, private router:Router
    ) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            if (params['searchTerm'])
              this.searchTerm = params['searchTerm'];
          })
    }

    search():void{
        if(this.searchTerm)
        this.router.navigateByUrl('/search/' + this.searchTerm);
      }


}