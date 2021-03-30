import { Injectable } from "@angular/core";
import { MyGlobals } from "../../../../my-globals";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { map } from "rxjs/operators";
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable()
export class AuthService {
  private _loginUrl = this._myGlobals.WebApiUrl + "api/auth";
  private _jwtHelper = new JwtHelperService();

  constructor(private _myGlobals: MyGlobals, private _http: HttpClient) {}

  login(model) {
    return this._http.post(this._loginUrl, model).pipe(
      map((res) => {
        this.handleSuccess(res);
      })
    );
  }

  logout() {
    localStorage.removeItem("auth_token");
  }

  private handleSuccess(response: any) {
    localStorage.setItem("auth_token", response.auth_token);
  }

  isAuthorized() {
    const token = localStorage.getItem("auth_token");
    return !this._jwtHelper.isTokenExpired(token);
  }
}
