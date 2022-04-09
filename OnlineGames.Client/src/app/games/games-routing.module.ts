import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { TicTacToeResolver } from "./tic-tac-toe/resolvers/tic-tac-toe.resolver";

const routes: Routes = [
    {
        path:"",
        pathMatch:"full",
        redirectTo:"tictactoe"

    },
    {
      path:"tictactoe",
      loadChildren:()=>import("./tic-tac-toe/tic-tac-toe-routing.module").then(t=>t.TicTacToeRouting),
      resolve:[TicTacToeResolver]
    },
    {
        path:"connect4",
        loadChildren:()=>import("./connect4/connect4-routing.module").then(c=>c.Connect4Routing)
      },
]; 
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class GamesRouting { }
  