import { Injectable } from '@angular/core';
import { MyGlobals } from "../../../my-globals";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({ 
        'Content-Type': 'application/json',
        'Authorization': ''
 })
};

@Injectable()
export class RolesService {
    private _rolesUrl = this._myGlobals.WebApiUrl + 'api/roles';

    constructor(private _myGlobals: MyGlobals,
                private _http: HttpClient) {
    }

    createAuthorizationHeader(headers: Headers) {
        let authToken = localStorage.getItem('auth_token');
        httpOptions.headers = httpOptions.headers.set('Authorization', `Bearer ${authToken}`);
    }

    getDropDownItems(): Observable<any> {
        let headers = new Headers();
        this.createAuthorizationHeader(headers);
        return this._http.get(this._rolesUrl + "/dropdown", httpOptions)
            .pipe(map(res => res));
    }
}