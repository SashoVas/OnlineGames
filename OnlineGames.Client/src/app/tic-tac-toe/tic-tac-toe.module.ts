import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TicTacToeComponent } from './tic-tac-toe/tic-tac-toe.component';
import { CoreModule } from '../core/core.module';
import { TicTacToeRoutingModule } from './tic-tac-toe-routing.module';

@NgModule({
  declarations: [
    TicTacToeComponent
  ],
  imports: [
    CommonModule,
    TicTacToeRoutingModule,
    CoreModule
  ],
  exports:[],
  providers:[]
})
export class TicTacToeModule { }
