import { Injectable } from "@angular/core";
import { MyGlobals } from "../../../my-globals";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { map } from "rxjs/operators";

const httpOptions = {
  headers: new HttpHeaders({
    "Content-Type": "application/json",
    Authorization: "",
  }),
};

@Injectable()
export class UsersService {
  private _usersUrl = this._myGlobals.WebApiUrl + "api/users";

  constructor(private _myGlobals: MyGlobals, private _http: HttpClient) {}

  createAuthorizationHeader(headers: Headers) {
    let authToken = localStorage.getItem("auth_token");
    httpOptions.headers = httpOptions.headers.set(
      "Authorization",
      `Bearer ${authToken}`
    );
  }

  getAll(): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http.get(this._usersUrl, httpOptions).pipe(map((res) => res));
  }

  getById(id): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .get(this._usersUrl + "/" + id, httpOptions)
      .pipe(map((res) => res));
  }

  insert(model): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .post(this._usersUrl, model, httpOptions)
      .pipe(map((res) => res));
  }

  update(model): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .put(this._usersUrl, model, httpOptions)
      .pipe(map((res) => res));
  }

  updatePassword(model): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .put(this._usersUrl + "/update-password", model, httpOptions)
      .pipe(map((res) => res));
  }

  delete(id): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .delete(this._usersUrl + "/" + id, httpOptions)
      .pipe(map((res) => res));
  }

  getLogedInUserData(): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .get(this._usersUrl + "/data", httpOptions)
      .pipe(map((res) => res));
  }
}
