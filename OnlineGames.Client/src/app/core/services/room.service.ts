import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  constructor(private http:HttpClient) { }
  createRoom():Observable<any>{
    return this.http.post(environment.apiUrl+'/room/createtictactoeroom',null)
  }
}
