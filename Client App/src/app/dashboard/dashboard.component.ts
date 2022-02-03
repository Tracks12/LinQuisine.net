import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  ing: string[] = ['Patates', 'Poireaux', 'Tomates', 'Abricot', 'Ver de terre'];
  constructor() { }

  ngOnInit(): void {
  }

}
