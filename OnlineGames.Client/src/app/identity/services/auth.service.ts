import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ILoginUser } from 'src/app/core/interfaces/ILoginUser';
import { IRegisterUser } from 'src/app/core/interfaces/IRegisterUser';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient) { }
  register(user:IRegisterUser):Observable<any>{
    return this.http.post(environment.apiUrl+'/register',user);
  }
  login(user:ILoginUser):Observable<any>{
    return this.http.post(environment.apiUrl+'/login',user);
  }
  getToken(){
    return localStorage.getItem('token'); 
  }
  saveToken(token:string){
    localStorage.setItem('token',token);
  }
}
