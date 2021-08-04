import { Order } from "./order";
import { ShopCart } from "./shopCart";

export interface User {

    id: number;

    name: string;

    serName: string;

    adress: string;

    phone: string;

    email: string;

    shopCart?: ShopCart;

    orders: Order[];

}
