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
export class AboutAuthorService {
  private _aboutAuthorUrl = this._myGlobals.WebApiUrl + "api/AboutAuthor";

  constructor(private _myGlobals: MyGlobals, private _http: HttpClient) {}

  createAuthorizationHeader(headers: Headers) {
    let authToken = localStorage.getItem("auth_token");
    httpOptions.headers = httpOptions.headers.set(
      "Authorization",
      `Bearer ${authToken}`
    );
  }

  insert(model): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .post(this._aboutAuthorUrl, model, httpOptions)
      .pipe(map((res) => res));
  }

  getById(id): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .get(this._aboutAuthorUrl + "/" + id, httpOptions)
      .pipe(map((res) => res));
  }

  update(model): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .put(this._aboutAuthorUrl, model, httpOptions)
      .pipe(map((res) => res));
  }
}
