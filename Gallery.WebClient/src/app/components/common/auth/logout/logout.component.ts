import { Component, Output, EventEmitter } from '@angular/core';
import { AuthService } from "../auth.services";
import { GlobalEventsManager } from "../../global-event-manager";
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent {

  constructor(private _authService: AuthService,
              private _globalEventsManager: GlobalEventsManager,
              private _router: Router) { }

  logout() {
    this._authService.logout();
    this._globalEventsManager.isAuthorized.emit(false);
    this.goToHome();
  }

  goToHome(){
    this._router.navigate(['/pictures']);
  }
}
