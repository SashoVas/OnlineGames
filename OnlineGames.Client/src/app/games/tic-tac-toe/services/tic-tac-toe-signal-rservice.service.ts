import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { from, Observable } from 'rxjs';
import { IBoardCoordinates } from 'src/app/core/interfaces/IBoardCoordinates';
import { TicTacToeServiceService } from './tic-tac-toe-service.service';
import { environment } from 'src/environments/environment';
import { AccountService } from 'src/app/core/services/account.service';
@Injectable({
  providedIn: 'root'
})
export class TicTacToeSignalRServiceService {

    constructor(private ticTacToeService:TicTacToeServiceService, private accountService:AccountService) {
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

  public tellOponentAI(player:number){
    let boardString=this.ticTacToeService.board.map(x=>x.map(y=>{
      if (y==""){
        return "0";
      }
      else{
        return y=='X'?'1':'2';
      }
    })).join().replace(/,/g, '');
    console.log(boardString);
    this.hubConnection.invoke("MakeMoveAI",boardString,player);
  }
  public addToRoom(roomName?:string){
    this.hubConnection.invoke("AddToGroup",roomName);
  }
  public addOponentMoveListener(func:(coordinates:IBoardCoordinates)=>void){
    this.hubConnection.on("OponentMove",func);
  }
  public tellOponenet(row:number,col:number){
    this.hubConnection.invoke("MakeMoveOponent",row,col);
  }
  public clearBoard(){
    this.hubConnection.invoke("ClearBoard");
  }
}
