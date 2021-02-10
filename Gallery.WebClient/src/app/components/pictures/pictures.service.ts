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
const pictureFormData: FormData = new FormData();

@Injectable()
export class PicturesService {
  private _picturesUrl = this._myGlobals.WebApiUrl + "api/pictures";

  allImages = [];

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
      .post(this._picturesUrl, model, httpOptions)
      .pipe(map((res) => res));
  }

  search(model): Observable<any> {
    return this._http
      .post(this._picturesUrl + "/" + "search", model)
      .pipe(map((res) => res));
  }

  delete(id): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .delete(this._picturesUrl + "/" + id, httpOptions)
      .pipe(map((res) => res));
  }

  getById(id): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .get(this._picturesUrl + "/" + id, httpOptions)
      .pipe(map((res) => res));
  }

  getSingleById(id): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .get(this._picturesUrl + "/single/" + id, httpOptions)
      .pipe(map((res) => res));
  }

  update(model): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .put(this._picturesUrl, model, httpOptions)
      .pipe(map((res) => res));
  }
}
