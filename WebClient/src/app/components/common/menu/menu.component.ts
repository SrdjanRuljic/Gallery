import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { UsersService } from "../../users/users.services";
import { AuthService } from "../auth/auth.services";

@Component({
  selector: "app-menu",
  templateUrl: "./menu.component.html",
  styleUrls: ["./menu.component.scss"],
})
export class MenuComponent implements OnInit {
  isAdmin: Observable<boolean>;
  displayName: Observable<string>;

  isAuthorized: Observable<boolean>;

  constructor(
    private _router: Router,
    private _authService: AuthService,
    private _usersService: UsersService
  ) {
    this.isAuthorized = this._authService.getIsAuthorized();
    this.isAdmin = this._usersService.getIsAdmin();
    this.displayName = this._usersService.getDisplayName();
  }

  ngOnInit() {}

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
