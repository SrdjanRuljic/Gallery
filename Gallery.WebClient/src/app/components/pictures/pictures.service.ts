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

  getImages() {
    return (this.allImages = Imagesdelatils.slice(0));
  }

  insert(model): Observable<any> {
    let headers = new Headers();
    this.createAuthorizationHeader(headers);
    return this._http
      .post(this._picturesUrl, model, httpOptions)
      .pipe(map((res) => res));
  }

  search(): Observable<any> {
    return this._http
      .post(this._picturesUrl + "/" + "search", {})
      .pipe(map((res) => res));
  }
}

const Imagesdelatils = [
  { id: 1, url: "assets/images/User.jfif", title: "Naslov 1" },
  { id: 2, url: "assets/images/User.jfif", title: "Naslov 2" },
  { id: 2, url: "assets/images/User.jfif", title: "Naslov 2" },
  { id: 2, url: "assets/images/User.jfif", title: "Naslov 2" },
  { id: 2, url: "assets/images/User.jfif", title: "Naslov 2" },
  { id: 2, url: "assets/images/User.jfif", title: "Naslov 2" },
  { id: 2, url: "assets/images/User.jfif", title: "Naslov 2" },
  { id: 2, url: "assets/images/User.jfif", title: "Naslov 2" },
  { id: 2, url: "assets/images/User.jfif", title: "Naslov 2" },
  { id: 3, url: "assets/images/User.jfif", title: "Naslov 2" },
];
