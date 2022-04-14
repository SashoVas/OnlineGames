import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  
  constructor(private accountService:AccountService,private router:Router) {}

  ngOnInit(): void {
  }
  isLoged(){
    return this.accountService.getToken()!=null;
  }
  logOut(){
    this.accountService.logOut();
    this.router.navigate([""]);
  }
}
