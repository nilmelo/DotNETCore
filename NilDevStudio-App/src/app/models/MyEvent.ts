import { Lot } from './Lot';
import { SocialNetwork } from './SocialNetwork';
import { Speaker } from './Speaker';

export class MyEvent
{
	constructor() {}
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
