import { Component, Input, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { Plant } from 'src/app/models/plant';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PlantService } from 'src/app/services/plant.service';
import { ShopCartService } from 'src/app/services/shop-cart.service';
import { ShopCartItem } from 'src/app/models/shopCartItem';

@Component({
  selector: 'app-plant',
  templateUrl: './plant.component.html',
  styleUrls: ['./plant.component.scss']
})
export class PlantComponent implements OnInit {

  @Input() plant!: Plant



  constructor(
    private snackBar: MatSnackBar,
    private plantService: PlantService,
    private shopCartService: ShopCartService
  ) { }

  ngOnInit(): void {

  }

  addToCart() {

    this.shopCartService.addPlantToCart(this.setShopCartItem()).subscribe(result => {
      if (result) {
        this.snackBar.open('Plant is added to the cart', '', {
          duration: 2000,
        })
      }
      else {
        this.snackBar.open('Try again', '', {
          duration: 2000,
        })
      }
    }

    )

  }

  setShopCartItem(): ShopCartItem {
    return {
      id: 0,

      plant: this.plant,

      plantId: this.plant.id

    };
  }


}
