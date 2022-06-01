import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IMessage } from '../interfaces/IMessage';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private http:HttpClient) { }
  private changeFriendSubject=new Subject();
  triggerChangeFriend(friendUserName:string){
    this.changeFriendSubject.next({friendUserName});
  }
  getObservableForChangeFriend():Observable<any>{
    return this.changeFriendSubject.asObservable();
  }
  getMessages(page:number,friendUserName:string):Observable<any>{
    return this.http.get<Array<IMessage>>(environment.apiUrl+'/Message/'+page+'/'+friendUserName)
  }
}
