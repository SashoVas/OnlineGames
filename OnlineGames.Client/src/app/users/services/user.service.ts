import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IFriend } from 'src/app/core/interfaces/IFriend';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient ) { }
  getFriends():Observable<any>{
    return this.http.get<Array<IFriend>>(environment.apiUrl+'/User');
  }
  sendFriendRequest(friendUserName:string):Observable<any>{
    return this.http.post(environment.apiUrl+'/User',{friendUserName});
  }
}
