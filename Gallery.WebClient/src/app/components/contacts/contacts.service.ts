import { Injectable } from "@angular/core";
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { MyGlobals } from "../../../my-globals";
import { Observable } from "rxjs/Observable";
import { map } from "rxjs/operators";

const httpOptions = {
  headers: new HttpHeaders({
    "Content-Type": "application/json",
    Authorization: "",
  }),
};

@Injectable()
export class ContactsService {
  private _contactsUrl = this._myGlobals.WebApiUrl + "api/contacts";

  constructor(private _myGlobals: MyGlobals, private _http: HttpClient) {}

  createAuthorizationHeader(headers: Headers) {
    let authToken = localStorage.getItem("auth_token");
    httpOptions.headers = httpOptions.headers.set(
      "Authorization",
      `Bearer ${authToken}`
    );
  }

  getAll(): Observable<any> {
    return this._http.get(this._contactsUrl).pipe(map((res) => res));
  }

  getById(id): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .get(this._contactsUrl + "/" + id, httpOptions)
      .pipe(map((res) => res));
  }

  insert(model): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .post(this._contactsUrl, model, httpOptions)
      .pipe(map((res) => res));
  }

  update(model): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .put(this._contactsUrl, model, httpOptions)
      .pipe(map((res) => res));
  }

  delete(id): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .delete(this._contactsUrl + "/" + id, httpOptions)
      .pipe(map((res) => res));
  }
}
