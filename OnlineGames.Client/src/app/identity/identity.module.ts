import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { IdentityRoutingModule } from './identity-routing.module';
import { CoreModule } from '../core/core.module';
import { AuthService } from './services/auth.service';



@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    IdentityRoutingModule,
    CoreModule
  ],
  exports:[
    LoginComponent,
    RegisterComponent
  ],
  providers:[AuthService]
})
export class IdentityModule { }
