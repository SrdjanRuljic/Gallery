import { Injectable } from '@angular/core';
import { MyGlobals } from "../../../../my-globals";
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthService {

    private _loginUrl = this._myGlobals.WebApiUrl + 'api/login';

    isLoggedIn: boolean = false;

    constructor(private _myGlobals: MyGlobals,
        private _http: HttpClient) {
        this.isLoggedIn = !!localStorage.getItem('auth_token');        
    }

    login(model) {
        return this._http.post(this._loginUrl, model)
        .pipe(map(res => this.handleSuccess(res)));
    }

    logout() {
        localStorage.removeItem('auth_token');
        this.isLoggedIn = false;
    }

    private handleSuccess(response: any){
        localStorage.setItem('auth_token', response.auth_token);
        this.isLoggedIn = true;
    }
}