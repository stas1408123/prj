import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ShopCart } from 'src/app/models/shopCart';
import { ShopCartItem } from 'src/app/models/shopCartItem';
import { ShopCartService } from 'src/app/services/shop-cart.service';


@Component({
  selector: 'app-shopcart',
  templateUrl: './shopcart.component.html',
  styleUrls: ['./shopcart.component.scss']
})
export class ShopcartComponent implements OnInit {

  shopCart!: ShopCart;

  constructor(
    public dialogRef: MatDialogRef<ShopcartComponent>,
    private shopCartService: ShopCartService,
    private snackBar: MatSnackBar,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.shopCartService.getShopCart().subscribe(result => {
      this.shopCart = result
    },
      error => {

      })
  }

  calculatePrice(): number {

    let result: number = 0

    this.shopCart.shopItems?.forEach(shopItems => {
      result += shopItems.plant.price;
    })

    return result;
  }

  goToHome() {
    this.router.navigateByUrl('');
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  removeShopCartItem(shopCartItem: ShopCartItem) {
    this.shopCartService.deleteShopCartItem(shopCartItem).subscribe(result => {
      if (result) {
        this.shopCart.shopItems?.splice(this.shopCart.shopItems?.indexOf(shopCartItem), 1)
        this.snackBar.open('Item was deleted', '', {
          duration: 2000,
        })
      }
    },
      error => {
      })
  }

  buy() {
    this.shopCartService.buy(this.shopCart).subscribe(result => {
      if (result) {
        this.snackBar.open('Order complite', '', {
          duration: 2000,
        })
      }
    },
      error => {
        console.log(error)
      });

  }



}
