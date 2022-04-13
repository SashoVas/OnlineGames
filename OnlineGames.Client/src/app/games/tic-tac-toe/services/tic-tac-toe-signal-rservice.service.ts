import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { from, Observable } from 'rxjs';
import { IBoardCoordinates } from 'src/app/core/interfaces/IBoardCoordinates';
import { TicTacToeServiceService } from './tic-tac-toe-service.service';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/identity/services/auth.service';
@Injectable({
  providedIn: 'root'
})
export class TicTacToeSignalRServiceService {

    constructor(private ticTacToeService:TicTacToeServiceService, private authService:AuthService) {
   }

  hubConnection!: signalR.HubConnection;
  startConnection():Observable<any>{

    let token=this.authService.getToken();
    var options = {
      transport: signalR.HttpTransportType.WebSockets,
      logging: signalR.LogLevel.Information
      
    };

    this.hubConnection=new signalR.HubConnectionBuilder()
      .withUrl(environment.apiUrl+'/TicTacToe/?token='+token,options)
      .build();

      return from(this.hubConnection.start());
  }

  public tellOponentAI(player:number):Observable<any>{
    let boardString=this.ticTacToeService.board.map(x=>x.map(y=>{
      if (y==""){
        return "0";
      }
      else{
        return y=='X'?'1':'2';
      }
    })).join().replace(/,/g, '');
    console.log(boardString);
    return from(this.hubConnection.invoke("MakeMoveAI",boardString,player));
  }
  public addToRoom(roomName:string):Observable<any>{
    return from(this.hubConnection.invoke("AddToGroup",roomName));
  }
  public addDataListeners(func:(coordinates:IBoardCoordinates)=>void){
    this.hubConnection.on("OponentMove",func);
  }
  public tellOponenet(row:number,col:number){
    return from(this.hubConnection.invoke("MakeMoveOponent",row,col))
  }
}
