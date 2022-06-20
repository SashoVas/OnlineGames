import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IUser } from '../interfaces/IUser';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http:HttpClient) { }

  logOut(){
    localStorage.removeItem('token');
  }
  getToken(){
    return localStorage.getItem('token'); 
  }
  saveToken(token:string){
    localStorage.setItem('token',token);
  }
  getUserCard():Observable<any>{
    return this.http.get<IUser>(environment.apiUrl+'/User/GetUserCard')
  }
}
