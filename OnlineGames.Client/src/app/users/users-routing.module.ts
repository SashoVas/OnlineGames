import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { FriendsComponent } from "./friends/friends.component";


const routes: Routes = [
    {
        path:"",
        pathMatch:"full",
        redirectTo:"friends"
    },
    {
      path:"friends",
      component:FriendsComponent
    }
    ]; 
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class UsersRoutingModule { }