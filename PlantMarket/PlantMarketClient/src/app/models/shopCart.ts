import { User } from "./user";
import { ShopCartItem } from "./shopCartItem";


export interface ShopCart {

       id: number;

       price: number;

       userId: number;

       user: User;

       shopItems?: ShopCartItem[]

}
