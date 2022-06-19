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

  createRoom(game:string):Observable<any>{
    return this.http.post(environment.apiUrl+'/room',{game:game});
  }
  getAvailableRooms(game:string|null,count:number,page:number):Observable<Array<IRoom>>{
    return this.http.get<Array<IRoom>>(environment.apiUrl+'/room?game='+game+'&count='+count+'&page='+page);
  }
  setUserToRoom(roomId:string){
    return this.http.put<IRoom>(environment.apiUrl+'/room',{roomId:roomId})
  }
}
