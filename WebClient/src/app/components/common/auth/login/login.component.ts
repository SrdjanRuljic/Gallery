import { Component } from "@angular/core";
import { AuthService } from "../auth.services";
import { GlobalEventsManager } from "../../global-event-manager";
import { ToastService } from "../../../common/toast/toast.service";
import { Router } from "@angular/router";
import { UsersService } from "../../../users/users.services";

export class Login {
  username: string;
  password: string;
}

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent {
  model: Login;

  constructor(
    private _authService: AuthService,
    private _globalEventsManager: GlobalEventsManager,
    private _toastService: ToastService,
    private _router: Router,
    private _usersService: UsersService
  ) {
    this.model = new Login();
  }

  login(username, password) {
    this.model.username = username;
    this.model.password = password;

    this._authService.login(this.model).subscribe((response) => {
      this.getLogedInUserData();
    });
  }

  getLogedInUserData() {
    this._usersService.getLogedInUserData().subscribe((response) => {
      this._globalEventsManager.isAdmin.emit(response.isAdmin);
      this._globalEventsManager.displayName.emit(response.displayName);
    });
  }

  goToHome() {
    this._router.navigate(["/pictures"]);
  }
}
