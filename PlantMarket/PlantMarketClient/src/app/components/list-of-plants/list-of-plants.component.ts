import { Component, OnInit,Input } from '@angular/core';
//import { Console } from 'console';
import { Category } from 'src/app/models/category';
import { Plant } from 'src/app/models/plant';
import { PlantService } from 'src/app/services/plant.service';

@Component({
  selector: 'app-list-of-plants',
  templateUrl: './list-of-plants.component.html',
  styleUrls: ['./list-of-plants.component.scss']
})
export class ListOfPlantsComponent implements OnInit {

  plants!:Plant[];

  constructor(
    private plantService:PlantService        
  ) { }

  ngOnInit(): void {

    this.plantService.getFavPlants().subscribe(result => {
      this.plants=result
    },
      error => {

    })

    this.plantService.selectedCategory.subscribe((result) =>{
      if(result){
        this.plantService.getAllPalantInCategory(result).subscribe(result=>{
          this.plants=result;
        })
      }
    })   
    this.plantService.selectedPlants.subscribe((result) =>{
      if(result){
        this.plants=result;
        }
    })  
    
  }

}
