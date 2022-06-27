import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './navigation/navigation.component';
import { RouterModule } from '@angular/router';
import { BoardCellComponent } from './board-cell/board-cell.component';
import { ChatComponent } from './chat/chat.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    NavigationComponent,
    BoardCellComponent,
    ChatComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule

  ],
  exports:[
    BoardCellComponent,
    NavigationComponent,
    ChatComponent
  ],
  providers: [],
})
export class CoreModule { }
