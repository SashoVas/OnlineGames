import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr'

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private toastr:ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((err)=>{
        let message="";
        if(err.status === 400)
        {
          message='Invalid parameters';
        }
        if(err.status===401)
        {
          message='Please login';
        }
        this.toastr.error(message);
        
        
        return throwError(err);
      })
      );
  }
}
