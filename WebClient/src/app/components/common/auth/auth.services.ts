import { Injectable } from "@angular/core";
import { MyGlobals } from "../../../../my-globals";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { map } from "rxjs/operators";
import { JwtHelperService } from "@auth0/angular-jwt";
import { BehaviorSubject, Observable } from "rxjs";
import { UsersService } from "../../users/users.services";

@Injectable()
export class AuthService {
  private _loginUrl = this._myGlobals.WebApiUrl + "api/auth";
  private _jwtHelper = new JwtHelperService();

  private isAuthorized = new BehaviorSubject<boolean>(this.hasToken());

  constructor(
    private _myGlobals: MyGlobals,
    private _http: HttpClient,
    private _usersServices: UsersService
  ) {}

  login(model) {
    return this._http.post(this._loginUrl, model).pipe(
      map((res) => {
        this.handleSuccess(res);
      })
    );
  }

  logout() {
    localStorage.removeItem("auth_token");
    this.isAuthorized.next(false);

    localStorage.removeItem("is_admin");
    this._usersServices.setIsAdmin(false);
  }

  private handleSuccess(response: any) {
    localStorage.setItem("auth_token", response.auth_token);
    this.isAuthorized.next(true);
  }

  hasToken() {
    const token = localStorage.getItem("auth_token");
    return !this._jwtHelper.isTokenExpired(token);
  }

  getIsAuthorized(): Observable<boolean> {
    return this.isAuthorized.asObservable();
  }
}
