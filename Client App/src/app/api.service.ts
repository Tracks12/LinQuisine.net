import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Recipe } from './recipe';


@Injectable({
  providedIn: 'root'
})
export class ApiService {
  API_URL: string = 'http://localhost:5000/api/';
  constructor(private httpClient: HttpClient) { }

  public getStatus(){
    return this.httpClient.get<any>(`${this.API_URL}status`);
  }

  getAllRecipes(): Observable<Recipe[]>{
    return this.httpClient.get<Recipe[]>(`${this.API_URL}recipes`);
  }

  createRecipe(data: Recipe): Observable<any>{
    return this.httpClient.post(`${this.API_URL}recipes`, data)
  }
  deleteRecipebById(id:any): Observable<any>{
    return this.httpClient.delete(`${this.API_URL}recipes/id/${id}`)
  }
  findbyName(name: any): Observable<Recipe[]>{
    return this.httpClient.get<Recipe[]>(`${this.API_URL}recipes/name/?name=${name}`)
  }

}
