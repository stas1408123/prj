import { Order } from "./order";
import { Plant } from "./plant";


export interface OrderedPlant {

    id: number;

    plant: Plant;

    plantId: number;

    Order?: Order;

    OrderId?: number;

}