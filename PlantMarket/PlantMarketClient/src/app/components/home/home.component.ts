import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Plant } from 'src/app/models/plant';
import { PlantService } from 'src/app/services/plant.service';
import { NavComponent } from '../nav/nav.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  plants!:Plant[]

  constructor(
    private plantService:PlantService
  ) { }

  ngOnInit(): void {
    
  }
}
