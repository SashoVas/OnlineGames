import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { Connect4GameComponent } from "./connect4-game/connect4-game.component";

const routes: Routes = [
    {
        path:"",
        pathMatch:"full",
        component:Connect4GameComponent,
    }]; 
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class Connect4RoutingModule { }