import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { observable, Observable, Subject } from 'rxjs';
import { Category } from '../models/category';
import { Plant } from '../models/plant';

@Injectable({
  providedIn: 'root'
})
export class PlantService {

  private url ='/api/Plant/';

  public selectedCategory:Subject<Category> =new Subject<Category>()

  public selectedPlants:Subject<Plant[]> =new Subject<Plant[]>() 
  
  constructor(public http: HttpClient) { }

  getAllPlants(): Observable<Plant[]>{

    return this.http.get<Plant[]>(`${this.url}GetAllProducts`);
  }

  getplantById(id :number):Observable<Plant>{
    return this.http.get<Plant>(`${this.url}GetPlant/?id=${id}`)
  }

  search(name : string):Observable<Plant[]>{
    return this.http.get<Plant[]>(`${this.url}Search/?name=${name}`)
  }

  addPlant(newPlant :Plant) : Observable<Plant>{
    return this.http.post<Plant>(`${this.url}`,newPlant);
  }

  deletePlantById(id:number) :Observable<boolean>{
    return this.http.delete<boolean>(`${this.url}DetetPlant/${id}`)
  }


  updatePlant(UpdatedPlant: Plant): Observable<Plant>{
    return this.http.put<Plant>(`${this.url}`,UpdatedPlant);
  }

  getFavPlants(): Observable<Plant[]>{

    return this.http.get<Plant[]>(`${this.url}GetFavPlants`);
  }

  getAllPalantInCategory(category :Category) : Observable<Plant[]>{
    return this.http.post<Plant[]>(`${this.url}GetAllPalantInCategory`,category);
  }


}
