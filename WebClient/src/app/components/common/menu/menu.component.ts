import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../auth/auth.services";
import { GlobalEventsManager } from "../global-event-manager";

@Component({
  selector: "app-menu",
  templateUrl: "./menu.component.html",
  styleUrls: ["./menu.component.scss"],
})
export class MenuComponent implements OnInit {
  isAdmin: boolean = false;
  displayName: string = null;

  isAuthorized: Observable<boolean>;

  constructor(
    private _router: Router,
    private _globalEventsManager: GlobalEventsManager,
    private _authService: AuthService
  ) {
    this.isAuthorized = _authService.isAuthorized();
    this.getLocalUserData();
  }

  ngOnInit() {}

  // isAuthorized() {
  //   return this._authService.isAuthorized();
  // }

  getLocalUserData() {
    this._globalEventsManager.isAdmin.subscribe((mode: boolean) => {
      this.isAdmin = mode;
    });
    this._globalEventsManager.displayName.subscribe((mode: string) => {
      this.displayName = mode;
    });
  }

  goToAboutAuthor() {
    this._router.navigate(["/about-author"]);
  }

  goToContacts() {
    this._router.navigate(["/contacts"]);
  }

  goToHome() {
    this._router.navigate(["/"]);
  }

  goToCategories() {
    this._router.navigate(["/categories"]);
  }

  goToUsers() {
    this._router.navigate(["/users"]);
  }
}
