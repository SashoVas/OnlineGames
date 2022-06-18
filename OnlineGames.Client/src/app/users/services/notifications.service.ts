import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { INotifications } from 'src/app/core/interfaces/INotifications';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  constructor(private http:HttpClient) { }

  getNorifications():Observable<any>{
    return this.http.get<INotifications>(environment.apiUrl+'/notification');
  }
  
}
