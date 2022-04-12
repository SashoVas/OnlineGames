import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/identity/services/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  
  constructor(private authService:AuthService,private router:Router) {}

  ngOnInit(): void {
  }
  isLoged(){
    return this.authService.getToken()!=null;
  }
  logOut(){
    this.authService.logOut();
    this.router.navigate([""]);
  }
}
