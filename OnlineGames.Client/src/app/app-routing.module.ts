import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IdentityGuard } from './identity/guards/identity.guard';
import { TicTacToeResolver } from './tic-tac-toe/resolvers/tic-tac-toe.resolver';
import { Connect4Resolver } from './connect4/resolvers/connect4.resolver';


const routes: Routes = [
  {
    path:"",
    pathMatch:"full",
    redirectTo:"home"
  }
  ,{
    path:"home",
    loadChildren:()=>import("./home/home.module").then(h=>h.HomeModule)
  },
  {
    path:"tictactoe",
    loadChildren:()=>import("./tic-tac-toe/tic-tac-toe.module").then(t=>t.TicTacToeModule),
    resolve:[TicTacToeResolver]
  },
  {
    path:"connect4",
    loadChildren:()=>import("./connect4/connect4.module").then(c=>c.Connect4Module),
    resolve:[Connect4Resolver]
  },
  {
    path:"identity",
    loadChildren:()=>import("./identity/identity.module").then(i=>i.IdentityModule),
    canActivate:[IdentityGuard]
  }
]; 

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
