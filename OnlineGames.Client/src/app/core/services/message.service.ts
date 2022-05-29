import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor() { }
  private changeFriendSubject=new Subject();
  triggerChangeFriend(friendUserName:string){
    console.log("change friend")
    this.changeFriendSubject.next({friendUserName});
  }
  getObservableForChangeFriend():Observable<any>{
    console.log('get observable');
    return this.changeFriendSubject.asObservable();
  }
}
