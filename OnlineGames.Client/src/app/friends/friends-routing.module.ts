import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ChatResolver } from "../core/resolvers/chat.resolver";
import { FriendsComponent } from "./friends/friends.component";


const routes: Routes = [
    {
        path:"",
        pathMatch:"full",
        redirectTo:"friends"
    },
    {
        path:"friends",
        component:FriendsComponent,
        resolve:[ChatResolver]
      },
    ]; 
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class FriendsRoutingModule { }