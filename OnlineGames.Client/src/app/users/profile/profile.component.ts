import { Component, OnInit } from '@angular/core';
import { IUser } from 'src/app/core/interfaces/IUser';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user!:IUser
  constructor(private userService:UserService) { }

  ngOnInit(): void {
    this.userService
      .getUser(null)
      .subscribe((data)=>this.user=data)
  }

}
