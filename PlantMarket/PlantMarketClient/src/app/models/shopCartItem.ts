import { Plant } from "./plant";
import { ShopCart } from "./shopCart";




export interface ShopCartItem {

    id: number;

    plant: Plant;

    plantId: number;

    shopCart?: ShopCart;

    shopCartId?: number;

}