import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/core/interfaces/IUser';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }

  getUser(id:string|null):Observable<any>
  {
    if (id==null)
    {
      return this.http.get<IUser>(environment.apiUrl+'/User')

    }
    return this.http.get<IUser>(environment.apiUrl+'/User/'+id)
  }
}
