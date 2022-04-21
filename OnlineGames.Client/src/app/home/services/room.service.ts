import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IRoom } from '../../core/interfaces/IRoom';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  constructor(private http:HttpClient) { }
  createRoom():Observable<any>{
    return this.http.post(environment.apiUrl+'/room/createtictactoeroom',null);
  }
  getAvailableRooms():Observable<Array<IRoom>>{
    return this.http.get<Array<IRoom>>(environment.apiUrl+'/room/getrooms');
  }
  setUserToRoom(roomId:string){
    return this.http.post(environment.apiUrl+'/room/addtoroom',{roomId:roomId})
  }
}
