import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModesComponent } from './modes/modes.component';
import { GamesRoutingModule } from './games-routing.module';

@NgModule({
  declarations: [
    ModesComponent
  ],
  imports: [
    CommonModule,
    GamesRoutingModule
  ]
})
export class GamesModule { }
