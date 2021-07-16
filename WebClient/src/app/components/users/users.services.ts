import { Injectable } from "@angular/core";
import { MyGlobals } from "../../../my-globals";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { BehaviorSubject, Observable } from "rxjs";
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

  private isAdmin$ = new BehaviorSubject<boolean>(this.isUserAdmin());
  private displayName$ = new BehaviorSubject<string>(this.userDisplayName());

  constructor(private _myGlobals: MyGlobals, private _http: HttpClient) {}

  createAuthorizationHeader(headers: Headers) {
    let authToken = localStorage.getItem("auth_token");
    httpOptions.headers = httpOptions.headers.set(
      "Authorization",
      `Bearer ${authToken}`
    );
  }

  getAll(model): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .post(this._usersUrl + "/get-all", model, httpOptions)
      .pipe(map((res) => res));
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
      .pipe(map((res) => this.handleSuccess(res)));
  }

  private handleSuccess(response: any) {
    localStorage.setItem("is_admin", response.isAdmin);
    this.setIsAdmin(response.isAdmin);

    localStorage.setItem("display_name", response.displayName);
    this.setDisplayName(response.displayName);
  }

  isUserAdmin() {
    return JSON.parse(localStorage.getItem("is_admin"));
  }

  getIsAdmin() {
    return this.isAdmin$.asObservable();
  }

  setIsAdmin(isAdmin: boolean) {
    this.isAdmin$.next(isAdmin);
  }

  userDisplayName() {
    return localStorage.getItem("display_name");
  }

  getDisplayName() {
    return this.displayName$.asObservable();
  }

  setDisplayName(name: string) {
    this.displayName$.next(name);
  }
}
