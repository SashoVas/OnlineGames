import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IFriend } from 'src/app/core/interfaces/IFriend';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent implements OnInit {
  friends!:Array<IFriend>;
  constructor(private userService:UserService ) { }

  ngOnInit(): void {
    this.userService.getFriends().subscribe(data=>this.friends=data);
  }

}
