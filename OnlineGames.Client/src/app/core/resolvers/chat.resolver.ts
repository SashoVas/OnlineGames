import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { MessageSignalRService } from '../services/message-signal-r.service';

@Injectable({
  providedIn: 'root'
})
export class ChatResolver implements Resolve<boolean> {
  constructor(private chatSignalRService:MessageSignalRService){}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.chatSignalRService.startConnection();
  }
}
