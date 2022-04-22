import { Component, OnInit } from '@angular/core';
import { Connect4ServiceService } from '../services/connect4-service.service';

@Component({
  selector: 'app-connect4-game',
  templateUrl: './connect4-game.component.html',
  styleUrls: ['./connect4-game.component.css'],
  providers:[Connect4ServiceService]
})
export class Connect4GameComponent implements OnInit {
  board:string[][];
  constructor(private connect4Service:Connect4ServiceService) {
    this.board=connect4Service.board;

   }

  ngOnInit(): void {
  }
  makeMove(row:number):void{
    this.connect4Service.makeMove(row);
  }
  newGame(){
    this.connect4Service.newGame();
    this.board=this.connect4Service.board;
  }
  gameEnded(){
    return this.connect4Service.checkWin();
  }
  getWinner(){
    return -this.connect4Service.currentPlayer==1?"O":"X";
  }
}
