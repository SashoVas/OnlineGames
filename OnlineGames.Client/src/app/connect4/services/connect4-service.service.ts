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
  gameEnded=false;
  draw:boolean=false;
  constructor() { }

  makeMove(row:number):void
  {
    if(this.gameEnded)
    {
      return;
    }
    for(let i=5;i>=0;i--)
    {
      if(this.board[i][row]!="O"&&this.board[i][row]!="X")
      {
        this.board[i][row]= this.currentPlayer==1?"O":"X";
        this.currentPlayer=-this.currentPlayer;
        this.gameEnded=this.checkWin();
        return;
      }
    }    
  }
  newGame(){
    this.board=[
      [ "", "", "", "", "", "", ""],
      [ "", "", "", "", "", "", ""],
      [ "", "", "", "", "", "", ""],
      [ "", "", "", "", "", "", ""],
      [ "", "", "", "", "", "", ""],
      [ "", "", "", "", "", "", ""]];
    this.currentPlayer=1;
    this.gameEnded=false;
  }
  checkWin():boolean{
    let currentSymbol=this.currentPlayer!=1?"O":"X";
    let isFull:boolean=false;
    //horizontal
    for (let i = 0; i < 6; i++)
    {
      
        for (let j = 0; j < 7-3; j++)
        {
            if (this.board[i][j]==currentSymbol&&this.board[i][j+1]==currentSymbol&&this.board[i][j+2]==currentSymbol&&this.board[i][j+3]==currentSymbol)
            {
              return true;
            }

        }
        isFull=this.board[i].includes("")||isFull;
    }
    //vertical
    for (let i = 0; i < 6-3; i++)
    {
        for (let j = 0; j < 7 ; j++)
        {
            if (this.board[i][j]==currentSymbol&&this.board[i+1][j]==currentSymbol&&this.board[i+2][j]==currentSymbol&&this.board[i+3][j]==currentSymbol)
            {
              return true;
            }
        }
    }
    //diagonal right
    for (let i = 0; i < 6 - 3; i++)
    {
        for (let j = 0; j < 7-3; j++)
        {
            if (this.board[i][j]==currentSymbol&&this.board[i+1][j+1]==currentSymbol&&this.board[i+2][j+2]==currentSymbol&&this.board[i+3][j+3]==currentSymbol)
            {
              return true;
            }
        }
    }
    //diagonal left
    for (let i = 4; i < 6 ; i++)
    {
        for (let j = 0; j < 7 - 3; j++)
        {
            if (this.board[i][j]==currentSymbol&&this.board[i-1][j+1]==currentSymbol&&this.board[i-2][j+2]==currentSymbol&&this.board[i-3][j+3]==currentSymbol)
            {
              return true;
            }
        }
    }
    this.draw=!isFull;
    return !isFull;
  }
}
