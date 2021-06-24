import { Component } from "@angular/core";
import { AuthService } from "../auth.services";
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
    this._usersService.getLogedInUserData().subscribe((response) => response);
  }

  goToHome() {
    this._router.navigate(["/pictures"]);
  }
}
