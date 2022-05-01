import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './navigation/navigation.component';
import { RouterModule } from '@angular/router';
import { BoardCellComponent } from './board-cell/board-cell.component';


@NgModule({
  declarations: [
    NavigationComponent,
    BoardCellComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[
    BoardCellComponent,
    NavigationComponent
  ],
  providers: [],
})
export class CoreModule { }
