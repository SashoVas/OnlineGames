import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { from, Observable } from 'rxjs';
import { IBoardCoordinates } from 'src/app/core/interfaces/IBoardCoordinates';
import { TicTacToeServiceService } from './tic-tac-toe-service.service';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class TicTacToeSignalRServiceService {

    constructor(private ticTacToeService:TicTacToeServiceService) {
   }

  hubConnection!: signalR.HubConnection;
  startConnection():Observable<any>{
    this.hubConnection=new signalR.HubConnectionBuilder()
      .withUrl(environment.apiUrl+'/TicTacToe',
      {
        skipNegotiation:true,
        transport:signalR.HttpTransportType.WebSockets
      }).
      build();

      return from(this.hubConnection.start());
  }

  public tellOponent(player:number):Observable<any>{
    let boardString=this.ticTacToeService.board.map(x=>x.map(y=>{
      if (y=="")
      {
        return "0";
      }
      else{
        return y=='X'?'1':'2';
      }
    })).join().replace(/,/g, '');
    console.log(boardString);
    return from(this.hubConnection.invoke("MakeMove",boardString,player))
  }

  public addDataListeners(func:(coordinates:IBoardCoordinates)=>void){
    this.hubConnection.on("OponentMove",func);
  }
}
