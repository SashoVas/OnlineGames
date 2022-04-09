import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { TicTacToeComponent } from "./tic-tac-toe/tic-tac-toe.component";

const routes: Routes = [
    {
        path:"",
        pathMatch:"full",
        redirectTo:"tictactoe"

    },
    {
      path:"tictactoe",
      component:TicTacToeComponent,
    },]; 
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class TicTacToeRouting { }
  