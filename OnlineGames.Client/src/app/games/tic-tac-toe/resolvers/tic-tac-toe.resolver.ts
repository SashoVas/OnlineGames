import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { TicTacToeSignalRServiceService } from '../services/tic-tac-toe-signal-rservice.service';

@Injectable({
  providedIn: 'root'
})
export class TicTacToeResolver implements Resolve<boolean> {
  constructor(private ticTacToeSignalRService:TicTacToeSignalRServiceService){}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.ticTacToeSignalRService.startConnection();
  }
}
