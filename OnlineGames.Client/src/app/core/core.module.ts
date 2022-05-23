import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './navigation/navigation.component';
import { RouterModule } from '@angular/router';
import { BoardCellComponent } from './board-cell/board-cell.component';
import { ChatComponent } from './chat/chat.component';


@NgModule({
  declarations: [
    NavigationComponent,
    BoardCellComponent,
    ChatComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[
    BoardCellComponent,
    NavigationComponent,
    ChatComponent
  ],
  providers: [],
})
export class CoreModule { }
