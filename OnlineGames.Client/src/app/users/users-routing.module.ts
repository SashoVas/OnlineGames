import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { NotificationsComponent } from "./notifications/notifications.component";
import { ProfileComponent } from "./profile/profile.component";
import { UserResolver } from "./resolvers/user.resolver";


const routes: Routes = [
    {
        path:"",
        pathMatch:"full",
        redirectTo:"profile"
    },
    {
      path:"profile",
      component:ProfileComponent,
      resolve:[UserResolver]
    },
    {
      path:"notifications",
      component:NotificationsComponent,
    },
    ]; 
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class UsersRoutingModule { }