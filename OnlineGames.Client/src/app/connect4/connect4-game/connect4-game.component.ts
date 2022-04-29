import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Connect4ServiceService } from '../services/connect4-service.service';
import { Connect4SignalRService } from '../services/connect4-signal-r.service';

@Component({
  selector: 'app-connect4-game',
  templateUrl: './connect4-game.component.html',
  styleUrls: ['./connect4-game.component.css'],
  providers:[Connect4ServiceService]
})
export class Connect4GameComponent implements OnInit,OnDestroy {
  board:string[][];
  roomId?:string=undefined;
  oponentTurn:boolean=false;
  startFirst:boolean=true;

  constructor(private connect4Service:Connect4ServiceService,private connect4SignalRService:Connect4SignalRService,private route:ActivatedRoute) {
    this.board=connect4Service.board;
    this.connect4SignalRService
      .addOponentMoveListener((col:number)=>{
        this.connect4Service.makeMove(col);
        this.oponentTurn=false;
      });
    this.connect4SignalRService.addClearBoardListener(()=>{this.clear();});
    this.route.queryParams.subscribe(params=>{
      if(params['roomName']!=null)
      {
        this.roomId=params['roomName'];
        this.tellOponent=(col:number)=>connect4SignalRService.tellOponenet(col);
        this.oponentTurn=params['first']=='false';
        this.startFirst=params['first']!='false';
      }
      this.connect4SignalRService.addToRoom(this.roomId);
    });
   }
  ngOnInit(): void {
  }
  makeMove(col:number):void{
    if(this.oponentTurn||this.gameEnded()){
      return ;
    }
    this.connect4Service.makeMove(col);
    this.oponentTurn=true;
    this.tellOponent(col);
  }
  tellOponent=(col:number)=>{
    this.connect4SignalRService
    .tellOponentAI(col);
  }
  clear(){
    this.connect4Service.newGame();
    this.board=this.connect4Service.board;
    this.startFirst=!this.startFirst;
    this.oponentTurn=!this.startFirst;
    if(!this.startFirst && this.roomId==undefined){
      this.connect4SignalRService.tellOponentAI(-1);
    }
  }
  gameEnded(){
    return this.connect4Service.checkWin();
  }
  getWinner(){
    if(this.connect4Service.draw)
    {
      return "Draw";
    }
    return -this.connect4Service.currentPlayer==1?"Blue":"Red";
  }
  newGameButtonClick(){
    this.connect4SignalRService.clearBoard();
  }
  ngOnDestroy(): void {
    this.connect4SignalRService.hubConnection.stop();
  }
}
