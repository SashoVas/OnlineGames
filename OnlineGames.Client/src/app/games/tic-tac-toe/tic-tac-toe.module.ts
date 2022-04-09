import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BoardCellComponent } from '../board-cell/board-cell.component';
import { TicTacToeComponent } from './tic-tac-toe/tic-tac-toe.component';



@NgModule({
  declarations: [
    BoardCellComponent,
    TicTacToeComponent],
  imports: [
    CommonModule
  ],
  exports:[BoardCellComponent]
})
export class TicTacToeModule { }
