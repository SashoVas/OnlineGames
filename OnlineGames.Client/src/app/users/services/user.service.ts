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

  getUser(name:string|null):Observable<any>
  {
    if (name==null)
    {
      return this.http.get<IUser>(environment.apiUrl+'/User')

    }
    return this.http.get<IUser>(environment.apiUrl+'/User/'+name)
  }
  updateUser(description:string,imgUrl:string,userName:string):Observable<any>
  {
    return this.http.put(environment.apiUrl+'/User',{description,imgUrl,userName})
  }
}
