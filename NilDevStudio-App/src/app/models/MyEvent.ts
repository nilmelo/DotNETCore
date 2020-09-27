import { Lot } from './Lot';
import { SocialNetwork } from './SocialNetwork';
import { Speaker } from './Speaker';

export interface MyEvent
{
    id: number;
    local: string;
    dateEvent: Date;
    theme: string;
    quantPeople: number;
    imageURL: string;
    telephone: string;
    email: string;
    lots: Lot[];
    socialNetworks: SocialNetwork[];
    EventSpeakers: Speaker[];
}
