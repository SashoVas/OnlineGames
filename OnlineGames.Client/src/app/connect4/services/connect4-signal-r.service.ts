import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { from, Observable } from 'rxjs';
import { AccountService } from 'src/app/core/services/account.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class Connect4SignalRService {

  constructor(private accountService:AccountService) {
  }

 hubConnection!: signalR.HubConnection;
 startConnection():Observable<any>{

   var options = {
     transport: signalR.HttpTransportType.WebSockets,
     logging: signalR.LogLevel.Information
     
   };

   this.hubConnection=new signalR.HubConnectionBuilder()
     .withUrl(environment.apiUrl+'/Connect4/?token='+this.accountService.getToken(),options)
     .build();

     return from(this.hubConnection.start());
 }
 public tellOponentAI(col:number,difficulty:number){
    
  this.hubConnection.invoke("MakeMoveAI",{col,difficulty});
}
 public addToRoom(roomName?:string){
   this.hubConnection.invoke("AddToGroup",roomName);
 }
 public addOponentMoveListener(func:(col:number)=>void){
   this.hubConnection.on("OponentMove",func);
 }
 public addClearBoardListener(func:()=>void){
   this.hubConnection.on("ClearBoard",func);
 }
 public tellOponenet(col:number){
   this.hubConnection.invoke("MakeMoveOponent",{col});
 }
 public clearBoard(){
   this.hubConnection.invoke("ClearBoard");
 }
}
