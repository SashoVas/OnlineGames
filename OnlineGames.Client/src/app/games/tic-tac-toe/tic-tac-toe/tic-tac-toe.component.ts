import { Component, OnChanges, OnDestroy, OnInit, SimpleChanges,ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, Observable, Subscription } from 'rxjs';
import { IBoardCoordinates } from 'src/app/core/interfaces/IBoardCoordinates';
import { TicTacToeServiceService } from '../services/tic-tac-toe-service.service';
import { TicTacToeSignalRServiceService } from '../services/tic-tac-toe-signal-rservice.service';

@Component({
  selector: 'app-tic-tac-toe',
  templateUrl: './tic-tac-toe.component.html',
  styleUrls: ['./tic-tac-toe.component.css']
})
export class TicTacToeComponent implements OnInit ,OnDestroy {
  first:boolean=true;
  board:string[][]=[];
  moveSubscription!:Subscription;
  gameEnded:boolean;
  inGame:boolean=false;
  roomName:string="";
  constructor(private ticTacToeService: TicTacToeServiceService,private ticTacToeSignalRService:TicTacToeSignalRServiceService,private route:ActivatedRoute) {
    this.board=ticTacToeService.board;
    
    this.ticTacToeSignalRService
      .addDataListeners((coordinates:IBoardCoordinates)=>{
        this.ticTacToeService.makeMove(coordinates.row,coordinates.col);
        this.gameEnded=this.ticTacToeService.gameEnded;
      });
      
    this.route.queryParams.subscribe(params=>{
      this.roomName=params['roomName'];
      if(this.roomName!=null)
      {
        this.tellOponent=(row:number,col:number)=>ticTacToeSignalRService.tellOponenet(row,col);
        this.ticTacToeSignalRService.addToRoom(this.roomName)
        .subscribe();
      }
      
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
    this.tellOponent(row,col);
  }
  playSecond(){
    this.first=false;
    this.inGame=true;
    this.tellOponent(0,0);
  }
  tellOponent=(row:number,col:number)=>{
    this.moveSubscription=this.ticTacToeSignalRService
    .tellOponentAI(this.ticTacToeService.currentPlayer)
    .subscribe();
  }
  clear(){
    this.first=true;
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