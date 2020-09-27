import { SocialNetwork } from './SocialNetwork';
import { MyEvent } from './MyEvent';

export interface Speaker
{
    id: number;
    name: string;
    curriculum: string;
    imageURL: string;
    telephone: string;
    email: string;
    socialNetworks: SocialNetwork[];
    EventSpeakers: MyEvent[];
}
