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
export class CategoriesService {
  private _categoriesUrl = this._myGlobals.WebApiUrl + "api/categories";

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
    return this._http
      .get(this._categoriesUrl, httpOptions)
      .pipe(map((res) => res));
  }

  getById(id): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .get(this._categoriesUrl + "/" + id, httpOptions)
      .pipe(map((res) => res));
  }

  insert(model): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .post(this._categoriesUrl, model, httpOptions)
      .pipe(map((res) => res));
  }

  update(model): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .put(this._categoriesUrl, model, httpOptions)
      .pipe(map((res) => res));
  }

  delete(id): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .delete(this._categoriesUrl + "/" + id, httpOptions)
      .pipe(map((res) => res));
  }

  getDropDownItems(): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .get(this._categoriesUrl + "/dropdown", httpOptions)
      .pipe(map((res) => res));
  }
}
