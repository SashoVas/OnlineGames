import { Component, OnChanges, OnDestroy, OnInit, SimpleChanges,ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
  inGame:boolean=false;
  oponentTurn:boolean=false;
  roomId?:string=undefined;
  constructor(private ticTacToeService: TicTacToeServiceService,private ticTacToeSignalRService:TicTacToeSignalRServiceService,private route:ActivatedRoute) {
    this.board=ticTacToeService.board;
    this.ticTacToeSignalRService
      .addOponentMoveListener((coordinates:IBoardCoordinates)=>{
        this.ticTacToeService.makeMove(coordinates.row,coordinates.col);
        this.oponentTurn=false;
      });
      
    this.route.queryParams.subscribe(params=>{
      if(params['roomName']!=null)
      {
        this.roomId=params['roomName'];
        this.tellOponent=(row:number,col:number)=>ticTacToeSignalRService.tellOponenet(row,col);
        this.oponentTurn=params['first']=='false';
      }
      this.ticTacToeSignalRService.addToRoom(this.roomId);
    });
   }

  ngOnInit(): void {
  }
  makeMove(row:number,col:number){ 
    if(this.gameEnded() || this.oponentTurn){
      return;
    }
    this.oponentTurn=true;
    this.inGame=true;
    this.ticTacToeService.makeMove(row,col);
    this.tellOponent(row,col);
  }
  playSecond(){
    this.oponentTurn=true;
    this.inGame=true;
    this.tellOponent(2,2);
  }
  tellOponent=(row:number,col:number)=>{
    this.ticTacToeSignalRService
    .tellOponentAI(this.ticTacToeService.currentPlayer);
  }
  clear(){
    this.ticTacToeSignalRService.clearBoard();
    this.oponentTurn=false;
    this.ticTacToeService.clear();
    this.board=this.ticTacToeService.board;
    this.inGame=false;
  }
  gameEnded(){
    return this.ticTacToeService.gameEnded;
  }
  ngOnDestroy():void{
    this.ticTacToeSignalRService.hubConnection.stop()
  }
}