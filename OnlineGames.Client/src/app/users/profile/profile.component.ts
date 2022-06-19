import { Component, ElementRef, OnInit,ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IUser } from 'src/app/core/interfaces/IUser';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user!:IUser
  constructor(private activatedRoute: ActivatedRoute,private userService:UserService ) { }
  @ViewChild('username')username!:ElementRef;
  ngOnInit(): void {
    this.activatedRoute.data.subscribe(data=>this.user=data[0])
  }
  updateUser($event:any){
    this.user=$event['user']
  }
  searchUser(){
    this.userService.getUser(this.username.nativeElement.value).subscribe(data=>this.user=data)
  }
}
