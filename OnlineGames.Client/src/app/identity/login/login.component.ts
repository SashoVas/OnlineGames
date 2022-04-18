import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/core/services/account.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers:[FormBuilder]
})
export class LoginComponent implements OnInit {
  loginForm:FormGroup;
  constructor(private fb:FormBuilder,private authService:AuthService,private router:Router,private accountService:AccountService) {
    this.loginForm=this.fb.group({
      'username':['',Validators.required],
      'password':['',Validators.required]
    });
   }

  ngOnInit(): void {
  }
  login(){
    this.authService.login(this.loginForm.value).subscribe((data)=>{
      this.accountService.saveToken(data['token']);
      this.router.navigate([""]);
    });
  }
  get username(){
    return this.loginForm.get('username');
  }
  get password(){
    return this.loginForm.get('password');
  }
}
