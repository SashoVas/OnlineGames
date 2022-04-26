import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { from, Observable } from 'rxjs';
import { IBoardCoordinates } from 'src/app/core/interfaces/IBoardCoordinates';
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
 public concect4HubTestSend(){
  this.hubConnection.invoke("TestAll");
 }
 public connect4HubTest(){
   this.hubConnection.on('Connect4HubTest',data=>console.log(data));
 }
 public tellOponentAI(col:number){
   
   this.hubConnection.invoke("MakeMoveAI",col);
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
   this.hubConnection.invoke("MakeMoveOponent",col);
 }
 public clearBoard(){
   this.hubConnection.invoke("ClearBoard");
 }
}
