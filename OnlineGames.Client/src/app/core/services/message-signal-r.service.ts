import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { from, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
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


 public sendMessageToRoom(roomId:string,contents:string){
   this.hubConnection.invoke("SendMessage",roomId,contents);
 }

}
