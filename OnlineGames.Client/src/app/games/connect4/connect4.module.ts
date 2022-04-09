import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Connect4GameComponent } from './connect4-game/connect4-game.component';
import { TicTacToeModule } from '../tic-tac-toe/tic-tac-toe.module';



@NgModule({
  declarations: [
    Connect4GameComponent
  ],
  imports: [
    CommonModule,
    TicTacToeModule
  ]
})
export class Connect4Module { }
