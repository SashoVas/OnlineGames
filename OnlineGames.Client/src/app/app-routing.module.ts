import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IdentityGuard } from './identity/guards/identity.guard';

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
    path:"friends",
    loadChildren:()=>import("./friends/friends.module").then(f=>f.FriendsModule)
  },
  {
    path:"identity",
    loadChildren:()=>import("./identity/identity.module").then(i=>i.IdentityModule),
    canActivate:[IdentityGuard]
  },
  {
    path:"users",
    loadChildren:()=>import("./users/users.module").then(u=>u.UsersModule)
  },
  {
    path:"games",
    loadChildren:()=>import("./games/games.module").then(u=>u.GamesModule)
  }
]; 

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
