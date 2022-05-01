import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {  Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers:[FormBuilder]
})
export class RegisterComponent implements OnInit {
  registerForm:FormGroup;
  constructor(private fb:FormBuilder,private authService:AuthService,private router:Router,private toastr:ToastrService) {
    this.registerForm=this.fb.group({
      'username':['',Validators.required],
      'password':['',Validators.required],
      'confirmPassword':['',Validators.required]
    });
   }

  ngOnInit(): void {
  }
  register(){
    this.authService.register(this.registerForm.value).subscribe((data)=>{
      this.toastr.success("Successfully registered")
      this.router.navigate([""],);
    });
  }
  get username(){
    return this.registerForm.get('username');
  }
  get password(){
    return this.registerForm.get('password');
  }
  get confirmPassword(){
    return this.registerForm.get('confirmPassword');
  }
}
