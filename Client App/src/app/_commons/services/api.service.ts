import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Recipe } from '../models/recipe';
import { Response } from '../models/response';
import { Login, LoginBody } from '../models/user';

@Injectable({
  providedIn: 'root'
})

export class ApiService {
	public headers: {} = {
		"Content-Type": "application/json",
		"authorization": "eyJpZCI6MTY0NDE3ODE2NywidXNlcm5hbWUiOiJhZG1pbiIsIm1haWwiOiJhZG1pbkBsb2NhbGhvc3QifQ=="
	};

  API_URL: string = 'http://localhost:5000/api';

	constructor(private httpClient: HttpClient) { }

	public getStatus(): Observable<any> {
		return this.httpClient
			.get<any>(`${this.API_URL}/status`);
	}

	public getVersion(): Observable<any> {
		return this.httpClient
			.get<any>(`${this.API_URL}/version`);
	}

	// Auth Part
	public login(data: LoginBody): Observable<Login> {
		return this.httpClient
			.post<Login>(`${this.API_URL}/auth/login`, data);
	}

	public register(data: any): Observable<any> {
		return this.httpClient
			.post<any>(`${this.API_URL}/auth/register`, data);
	}

	public logout(): Observable<any> {
		return this.httpClient
			.post<any>(`${this.API_URL}/auth/logout`, {}, { headers: this.headers });
	}

	// Recipes Part
	public getAllRecipes(): Observable<Recipe[]> {
		return this.httpClient
			.get<Recipe[]>(`${this.API_URL}/recipes`, { headers: this.headers });
	}

	public createRecipe(data: Recipe): Observable<Response> {
		return this.httpClient
			.post<Response>(`${this.API_URL}/recipes`, data, { headers: this.headers });
	}

	public putRecipebById(id: number, data: any): Observable<Recipe> {
		return this.httpClient
			.put<Recipe>(`${this.API_URL}/recipes/id/${id}`, data, { headers: this.headers });
	}

	public deleteRecipebById(id: number): Observable<Response> {
		return this.httpClient
			.delete<Response>(`${this.API_URL}/recipes/id/${id}`, { headers: this.headers });
	}

	public findbyName(name: string): Observable<Recipe> {
		return this.httpClient
			.get<Recipe>(`${this.API_URL}/recipes/name/${name}`, { headers: this.headers });
	}
}
