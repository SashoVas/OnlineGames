import { IFriend } from "./IFriend";
import { IMessage } from "./IMessage";

export interface INotifications{
    friendRequests:Array<IFriend>,
    messages:Array<IMessage>
}