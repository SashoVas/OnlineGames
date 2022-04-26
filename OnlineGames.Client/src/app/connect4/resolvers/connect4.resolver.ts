import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { Connect4SignalRService } from '../services/connect4-signal-r.service';

@Injectable({
  providedIn: 'root'
})
export class Connect4Resolver implements Resolve<boolean> {
  constructor(private connect4SignalRService:Connect4SignalRService){}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.connect4SignalRService.startConnection();
  }
}
