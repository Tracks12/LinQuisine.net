import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  API_URL: string = 'http://localhost:5000/api/';
  constructor(private httpClient: HttpClient) { }

  public getStatus(){
    return this.httpClient.get<any>(`${this.API_URL}status`);
  }

  listeRecipes(): Observable<any[]>{
    return this.httpClient.get<any>(`${this.API_URL}recipes`);
  }
}
