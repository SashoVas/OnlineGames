import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingComponent } from './landing/landing.component';


const routes: Routes = [
  {
    path:"",
    pathMatch:"full",
    component:LandingComponent,
  },
  {
    path:"games",
    loadChildren:()=>import("./games/games-routing.module").then(m=>m.GamesRouting)
  },
  {
    path:"identity",
    loadChildren:()=>import("./identity/identity-routing.module").then(i=>i.IdentityRouting)
  }

]; 

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
