export interface Lot
{
    id: number;
    name: string;
    price: number;
    dateStart?: Date;
    dateEnd?: Date;
    quantity: number;
    eventId: number;
}
