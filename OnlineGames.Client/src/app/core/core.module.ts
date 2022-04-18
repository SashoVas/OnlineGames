import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './navigation/navigation.component';
import { RouterModule } from '@angular/router';
import { LandingComponent } from './landing/landing.component';
import { HttpClientModule } from '@angular/common/http';
import { BoardCellComponent } from './board-cell/board-cell.component';




@NgModule({
  declarations: [
    NavigationComponent,
    LandingComponent,
    BoardCellComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule
  ],
  exports:[
    BoardCellComponent,
    LandingComponent,
    NavigationComponent
  ]
})
export class CoreModule { }
