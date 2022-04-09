import { Component, OnChanges, OnDestroy, OnInit, SimpleChanges,ViewChild } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { IBoardCoordinates } from 'src/app/core/interfaces/IBoardCoordinates';
import { TicTacToeServiceService } from '../services/tic-tac-toe-service.service';
import { TicTacToeSignalRServiceService } from '../services/tic-tac-toe-signal-rservice.service';

@Component({
  selector: 'app-tic-tac-toe',
  templateUrl: './tic-tac-toe.component.html',
  styleUrls: ['./tic-tac-toe.component.css']
})
export class TicTacToeComponent implements OnInit ,OnDestroy {

  board:string[][]=[];
  moveSubscription!:Subscription;
  gameEnded:boolean;
  inGame:boolean=false;

  constructor(private ticTacToeService: TicTacToeServiceService,private ticTacToeSignalRService:TicTacToeSignalRServiceService) {
    this.board=ticTacToeService.board;

    this.ticTacToeSignalRService
      .addDataListeners((coordinates:IBoardCoordinates)=>{
        this.ticTacToeService.makeMove(coordinates.row,coordinates.col);
        this.gameEnded=this.ticTacToeService.gameEnded;
      });

    this.gameEnded=this.ticTacToeService.gameEnded;
   }

  ngOnInit(): void {
  }
  makeMove(row:number,col:number){ 
    if(this.gameEnded){
      return;
    }
    this.inGame=true;
    this.ticTacToeService.makeMove(row,col);
    this.gameEnded=this.ticTacToeService.gameEnded;
    this.gameEnded=this.ticTacToeService.gameEnded;
    if(this.gameEnded){
      return;
    }
    this.moveSubscription=this.ticTacToeSignalRService
    .tellOponent(this.ticTacToeService.currentPlayer)
    .subscribe();
  }
  playSecond(){
    this.inGame=true;
    this.moveSubscription=this.ticTacToeSignalRService
    .tellOponent(this.ticTacToeService.currentPlayer)
    .subscribe();
  }
  clear(){
    this.ticTacToeService.clear();
    this.board=this.ticTacToeService.board;
    this.gameEnded=false;
    this.inGame=false;
  }
  ngOnDestroy():void{
    this.moveSubscription?.unsubscribe();
    this.ticTacToeSignalRService.hubConnection.stop()
  }
}