import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IRoom } from '../../core/interfaces/IRoom';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  constructor(private http:HttpClient) { }
  createRoomTicTacToe():Observable<any>{
    return this.http.post(environment.apiUrl+'/room/createtictactoeroom',null);
  }
  createRoomConnect4():Observable<any>{
    return this.http.post(environment.apiUrl+'/room/createconnect4room',null);
  }
  getAvailableRooms(game:string|null,count:number,page:number):Observable<Array<IRoom>>{
    return this.http.get<Array<IRoom>>(environment.apiUrl+'/room/getrooms?game='+game+'&count='+count+'&page='+page);
  }
  setUserToRoom(roomId:string){
    return this.http.post<IRoom>(environment.apiUrl+'/room/addtoroom',{roomId:roomId})
  }
}
