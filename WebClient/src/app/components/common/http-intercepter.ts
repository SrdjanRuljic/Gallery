import { Injectable } from "@angular/core";
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HTTP_INTERCEPTORS,
  HttpErrorResponse,
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { ToastService } from "./toast/toast.service";
import { AuthService } from "./auth/auth.services";
import { Router } from "@angular/router";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private _toastService: ToastService,
    private _authService: AuthService,
    private _router: Router
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error) => {
        if (error.status === 401) {
          this.handleUnauthorized();
          return throwError(error.statusText);
        }
        if (error instanceof HttpErrorResponse) {
          this.handleError(error.error);
          return throwError(error.error);
        }
      })
    );
  }

  handleError(message) {
    this._toastService.activate(message, "alert-danger");
  }

  handleUnauthorized() {
    this._authService.logout();
    this.goToHome();
    this._toastService.activate("Nemate prava pristupa.", "alert-danger");
  }

  goToHome() {
    this._router.navigate(["/pictures"]);
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true,
};
