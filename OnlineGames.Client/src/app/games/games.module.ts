import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TicTacToeModule } from './tic-tac-toe/tic-tac-toe.module';
import { Connect4Module } from './connect4/connect4.module';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    TicTacToeModule,
    Connect4Module
  ]
})
export class GamesModule { }
