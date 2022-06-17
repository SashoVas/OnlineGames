import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { GamesComponent } from "./games/games.component";
import { LandingComponent } from "./landing/landing.component";
import { RoomsComponent } from "./rooms/rooms.component";

const routes: Routes = [
    {
        path:"",
        pathMatch:"full",
        component:LandingComponent
    },
    {
      path:"games",
      component:GamesComponent
    },
    {
      path:"rooms",
      component:RoomsComponent
    }
    ]; 
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class HomeRoutingModule { }