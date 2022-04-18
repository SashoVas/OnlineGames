import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Connect4GameComponent } from './connect4-game/connect4-game.component';

import { CoreModule } from '../core/core.module';
import { Connect4RoutingModule } from './connect4-routing.module';
import { Connect4ServiceService } from './services/connect4-service.service';

@NgModule({
  declarations: [
    Connect4GameComponent,
    
  ],
  imports: [
    CommonModule,
    CoreModule,
    Connect4RoutingModule
  ],
  providers:[Connect4ServiceService]
})
export class Connect4Module { }
