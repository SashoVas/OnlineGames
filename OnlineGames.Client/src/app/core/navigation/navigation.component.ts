import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IUser } from '../interfaces/IUser';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  user!:IUser;
  constructor(private accountService:AccountService,private router:Router) {}

  ngOnInit(): void {
    if(this.isLoged()){
      this.accountService.getUserCard().subscribe(data=>this.user=data);
    }
  }
  isLoged(){
    return this.accountService.getToken()!=null;
  }
  logOut(){
    this.accountService.logOut();
    this.router.navigate([""]);
  }
}
