import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { Connect4Resolver } from "../connect4/resolvers/connect4.resolver";
import { ChatResolver } from "../core/resolvers/chat.resolver";
import { TicTacToeResolver } from "../tic-tac-toe/resolvers/tic-tac-toe.resolver";
import { ModesComponent } from "./modes/modes.component";

const routes: Routes = [
    {
        path:"",
        pathMatch:"full",
        redirectTo:"modes"
    },
    {
        path:"modes",
        component:ModesComponent
    },
    {
        path:"modes/tictactoe",
        loadChildren:()=>import("../tic-tac-toe/tic-tac-toe.module").then(t=>t.TicTacToeModule),
        resolve:[TicTacToeResolver,ChatResolver]
      },
      {
        path:"modes/connect4",
        loadChildren:()=>import("../connect4/connect4.module").then(c=>c.Connect4Module),
        resolve:[Connect4Resolver,ChatResolver]
      },
    ]; 
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class GamesRoutingModule { }