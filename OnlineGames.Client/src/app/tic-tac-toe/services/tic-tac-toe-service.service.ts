import { Injectable } from '@angular/core';

@Injectable()
export class TicTacToeServiceService {
  board:string[][]=[["","",""],["","",""],["","",""]];
  constructor() { }
  currentPlayer=1;
  gameEnded:boolean=false;
  draw:boolean=false;
  makeMove(row:number,col:number)
  {
    this.board[row][col]= this.currentPlayer==1?"X":"O";
    this.gameEnded=this.checkWin();
    this.currentPlayer=-this.currentPlayer;
  }
  clear()
  {
    this.board=[["","",""],["","",""],["","",""]];
    this.currentPlayer=1;
    this.gameEnded=false;
  }
  checkWin():boolean{
    let playerAvatar=((this.currentPlayer==1)?'X':'O');
    let isFull:boolean=false;
    for(let i=0;i<3;i++)
    {
      if(this.board[i].every(x=>x==playerAvatar)){//this.board[i][0]==playerAvatar&&this.board[i][1]==playerAvatar&&this.board[i][2]==playerAvatar){
        return true;
      }
      if(this.board[0][i]==playerAvatar&&this.board[1][i]==playerAvatar&&this.board[2][i]==playerAvatar){
        return true;
      }
      isFull=this.board[i].includes('')||isFull;
    }
    if(this.board[0][0]==playerAvatar&&this.board[1][1]==playerAvatar&&this.board[2][2]==playerAvatar){
      return true;
    }
    if(this.board[2][0]==playerAvatar&&this.board[1][1]==playerAvatar&&this.board[0][2]==playerAvatar){
      return true;
    }
    this.draw=!isFull;
    return !isFull;
  }
}
