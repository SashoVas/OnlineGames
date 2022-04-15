import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './navigation/navigation.component';
import { RouterModule } from '@angular/router';
import { LandingComponent } from './landing/landing.component';




@NgModule({
  declarations: [
    NavigationComponent,
    LandingComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[
    NavigationComponent,
    LandingComponent
  ]
})
export class CoreModule { }
