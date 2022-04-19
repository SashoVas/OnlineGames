import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { from, Observable } from 'rxjs';
import { IBoardCoordinates } from 'src/app/core/interfaces/IBoardCoordinates';
import { environment } from 'src/environments/environment';
import { AccountService } from 'src/app/core/services/account.service';
@Injectable({
  providedIn: 'root'
})
export class TicTacToeSignalRServiceService {

    constructor(private accountService:AccountService) {
   }

  hubConnection!: signalR.HubConnection;
  startConnection():Observable<any>{

    var options = {
      transport: signalR.HttpTransportType.WebSockets,
      logging: signalR.LogLevel.Information
      
    };

    this.hubConnection=new signalR.HubConnectionBuilder()
      .withUrl(environment.apiUrl+'/TicTacToe/?token='+this.accountService.getToken(),options)
      .build();

      return from(this.hubConnection.start());
  }

  public tellOponentAI(row:number,col:number){
    
    this.hubConnection.invoke("MakeMoveAI",row,col);
  }
  public addToRoom(roomName?:string){
    this.hubConnection.invoke("AddToGroup",roomName);
  }
  public addOponentMoveListener(func:(coordinates:IBoardCoordinates)=>void){
    this.hubConnection.on("OponentMove",func);
  }
  public addClearBoardListener(func:()=>void){
    this.hubConnection.on("ClearBoard",func);
  }
  public tellOponenet(row:number,col:number){
    this.hubConnection.invoke("MakeMoveOponent",row,col);
  }
  public clearBoard(){
    this.hubConnection.invoke("ClearBoard");
  }
}
