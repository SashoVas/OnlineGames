import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ChatResolver } from "../core/resolvers/chat.resolver";
import { FriendsComponent } from "./friends/friends.component";
import { ProfileComponent } from "./profile/profile.component";
import { UserResolver } from "./resolvers/user.resolver";


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
    {
      path:"profile",
      component:ProfileComponent,
      resolve:[UserResolver]
    },
    ]; 
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class UsersRoutingModule { }