import { Injectable } from '@angular/core';

@Injectable()
export class Connect4ServiceService {
  board:string[][]=[
    [ "", "", "", "", "", "", ""],
    [ "", "", "", "", "", "", ""],
    [ "", "", "", "", "", "", ""],
    [ "", "", "", "", "", "", ""],
    [ "", "", "", "", "", "", ""],
    [ "", "", "", "", "", "", ""],
];
  currentPlayer=1;
  constructor() { }
  makeMove(row:number):void
  {
    this.board[5][row]= this.currentPlayer==1?"O":"X";
    this.currentPlayer=-this.currentPlayer;
  }

}
