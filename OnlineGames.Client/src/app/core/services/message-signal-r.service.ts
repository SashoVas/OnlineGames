import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { from, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IMessage } from '../interfaces/IMessage';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class MessageSignalRService {

  constructor(private accountService:AccountService) {
  }

 hubConnection!: signalR.HubConnection;
 startConnection():Observable<any>{

   var options = {
     transport: signalR.HttpTransportType.WebSockets,
     logging: signalR.LogLevel.Information
     
   };

   this.hubConnection=new signalR.HubConnectionBuilder()
     .withUrl(environment.apiUrl+'/Chat/?token='+this.accountService.getToken(),options)
     .build();

     return from(this.hubConnection.start());
 }


  public sendMessageToRoom(groupName:string,contents:string,isName:boolean){
    this.hubConnection.invoke("SendMessage",{id:groupName,contents,isName});
 }
  public joinGroup(groupName:string,isName:boolean){
    this.hubConnection.invoke("JoinGroup",{id:groupName,isName});
  }
  public receiveMessage(func:(message:IMessage)=>void){
    this.hubConnection.on("ReceiveMessage",func);
  }
  public changeGroup(friendName:string,oldFriendName:string){
    this.hubConnection.invoke("ChangeGroup",{friendName,oldFriendName});
  }
  public readMessage(messageId:string){
    this.hubConnection.invoke("ReadMessage",{messageId});
  }
}
