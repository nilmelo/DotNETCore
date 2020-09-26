import { Lot } from './Lot';
import { SocialNetwork } from './SocialNetwork';
import { Speaker } from './Speaker';

export interface MyEvent
{
    id: number;
    local: string;
    dataEvent: Date;
    theme: string;
    quantPeople: number;
    imageURL: string;
    telephone: string;
    email: string;
    lots: Lot[];
    socialNetworks: SocialNetwork[];
    speakers: Speaker[];
}
