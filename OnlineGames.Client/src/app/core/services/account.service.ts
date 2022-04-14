import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor() { }

  logOut(){
    localStorage.removeItem('token');
  }
  getToken(){
    return localStorage.getItem('token'); 
  }
  saveToken(token:string){
    localStorage.setItem('token',token);
  }
}
