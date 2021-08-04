import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Order } from '../models/order';
import { ShopCart } from '../models/shopCart';
import { ShopCartItem } from '../models/shopCartItem';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class ShopCartService {

  private url ='/api/ShopCart/';

  constructor(public http: HttpClient) { }

  createShopCart(user:User): Observable<ShopCart>{

    return this.http.post<ShopCart>(`${this.url}CreateCart`,user);
  }

  getShopCart(): Observable<ShopCart>{

    return this.http.get<ShopCart>(`${this.url}GetShopCart`);
  }

  addPlantToCart(shopCartItem :ShopCartItem) : Observable<boolean>{
    return this.http.post<boolean>(`${this.url}AddPlantToCart`,shopCartItem);
  }

  deleteShopCartItem(shopCartItem:ShopCartItem): Observable<boolean>{

    return this.http.post<boolean>(`${this.url}DeleteShopCartItem`,shopCartItem);
  }

  buy(shopCart: ShopCart): Observable<Order>{

    return this.http.post<Order>(`${this.url}buy`,shopCart);
  }

}
