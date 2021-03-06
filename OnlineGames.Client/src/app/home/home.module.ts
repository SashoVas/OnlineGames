import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CoreModule } from '../core/core.module';
import { LandingComponent } from './landing/landing.component';
import { HomeRoutingModule } from './home-routing.module';

@NgModule({
  declarations: [LandingComponent],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    CoreModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
