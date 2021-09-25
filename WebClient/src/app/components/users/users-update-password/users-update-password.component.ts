import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { UpdatePassword } from "../update-password";
import { UsersService } from "../users.services";
import { ToastService } from "../../common/toast/toast.service";

@Component({
  selector: "app-users-update-password",
  templateUrl: "./users-update-password.component.html",
  styleUrls: ["./users-update-password.component.scss"],
})
export class UsersUpdatePasswordComponent implements OnInit {
  model: UpdatePassword;

  constructor(
    private _router: Router,
    private _route: ActivatedRoute,
    private _usersService: UsersService,
    private _toastService: ToastService
  ) {
    this.model = new UpdatePassword();
  }

  ngOnInit() {
    this._route.params.subscribe((params) => {
      let id = params["id"];
      this.initModel(id);
    });
  }

  initModel(id) {
    this.model.id = id;
    this.model.password = null;
    this.model.confirmedPassword = null;
  }

  save() {
    if (this.passwordValidation() && this.confirmedPasswordValidation()) {
      this.update();
    }
  }

  update() {
    this._usersService.updatePassword(this.model).subscribe((response) => {
      this._toastService.activate(
        "Lozinka je uspješno izmjenjena.",
        "alert-success"
      );
      this.goBack();
    });
  }

  passwordValidation() {
    return !!!(this.model.password == null || this.model.password.length < 1);
  }

  confirmedPasswordValidation() {
    return !!!(
      this.model.confirmedPassword == null ||
      this.model.confirmedPassword.length < 1
    );
  }

  comparePasswords() {
    return !!(this.model.confirmedPassword === this.model.password);
  }

  goBack() {
    this._router.navigate(["/users"]);
  }
}
