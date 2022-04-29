import { Component, OnChanges, OnDestroy, OnInit, SimpleChanges,ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IBoardCoordinates } from 'src/app/core/interfaces/IBoardCoordinates';
import { TicTacToeServiceService } from '../services/tic-tac-toe-service.service';
import { TicTacToeSignalRServiceService } from '../services/tic-tac-toe-signal-rservice.service';

@Component({
  selector: 'app-tic-tac-toe',
  templateUrl: './tic-tac-toe.component.html',
  styleUrls: ['./tic-tac-toe.component.css'],
  providers:[TicTacToeServiceService]
})
export class TicTacToeComponent implements OnInit ,OnDestroy {
  board:string[][]=[];
  oponentTurn:boolean=false;
  roomId?:string=undefined;
  startFirst:boolean=true;
  constructor(private ticTacToeService: TicTacToeServiceService,private ticTacToeSignalRService:TicTacToeSignalRServiceService,private route:ActivatedRoute) {    
    this.board=ticTacToeService.board;
    this.ticTacToeSignalRService
      .addOponentMoveListener((coordinates:IBoardCoordinates)=>{
        this.ticTacToeService.makeMove(coordinates.row,coordinates.col);
        this.oponentTurn=false;
      });
    this.ticTacToeSignalRService.addClearBoardListener(()=>{this.clear();});
    this.route.queryParams.subscribe(params=>{
      if(params['roomName']!=null)
      {
        this.roomId=params['roomName'];
        this.tellOponent=(row:number,col:number)=>ticTacToeSignalRService.tellOponenet(row,col);
        this.oponentTurn=params['first']=='false';
        this.startFirst=params['first']!='false';
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
    this.ticTacToeService.makeMove(row,col);
    this.tellOponent(row,col);
  }
  tellOponent=(row:number,col:number)=>{
    this.ticTacToeSignalRService
    .tellOponentAI(row,col);
  }
  clear(){
    this.ticTacToeService.clear();
    this.board=this.ticTacToeService.board;
    this.startFirst=!this.startFirst;
    this.oponentTurn=!this.startFirst;
    if(!this.startFirst && this.roomId==undefined){
      this.ticTacToeSignalRService.tellOponentAI(-1,-1);
    }
  }
  clearButtonClick()
  {
    this.ticTacToeSignalRService.clearBoard();
  }
  gameEnded(){
    return this.ticTacToeService.gameEnded;
  }
  getWinner(){
    if(this.ticTacToeService.draw)
    {
      return 'Draw';
    }
    return -this.ticTacToeService.currentPlayer==1?'X':'O';
  }
  ngOnDestroy():void{
    this.ticTacToeSignalRService.hubConnection.stop()
  }
}