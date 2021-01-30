import { Component, OnInit } from "@angular/core";
import { UsersService } from "../users.services";
import { Router } from "@angular/router";
import { User } from "../user";
import { ModalService } from "../../common/modal/modal.service";
import { ToastService } from "../../common/toast/toast.service";

@Component({
  selector: "app-users-list",
  templateUrl: "./users-list.component.html",
  styleUrls: ["./users-list.component.css"],
})
export class UsersListComponent implements OnInit {
  users: User[];
  itemsToDisplay: User[];
  itemsPerPage: number = 10;
  numberOfPages: number[] = [];
  currentPage: number = 0;

  constructor(
    private _usersService: UsersService,
    private _router: Router,
    private _modalService: ModalService,
    private _toastService: ToastService
  ) {
    this.users = [];
    this.itemsToDisplay = [];
  }

  ngOnInit() {
    this.getUsers();
  }

  initialize(data) {
    this.users = data;
    this.numberOfPages.length = Math.ceil(
      this.users.length / this.itemsPerPage
    );
    if (this.numberOfPages.length > 1) {
      this.numberOfPages = Array.from(Array(this.numberOfPages.length).keys());
    }
    this.changePage(this.currentPage);
  }

  changePage(pageNum) {
    this.currentPage = pageNum;
    this.itemsToDisplay = this.users.slice(
      this.currentPage * this.itemsPerPage,
      this.currentPage * this.itemsPerPage + this.itemsPerPage
    );
    if (this.itemsToDisplay.length == 0) {
      this._toastService.activate("Nema podataka za prikaz.", "alert-danger");
    }
  }

  getUsers() {
    this._usersService.getAll().subscribe((data) => {
      this.initialize(data);
    });
  }

  goToUserForm(id) {
    this._router.navigate(["/users/form", id]);
  }

  goToResetPassword(id) {
    this._router.navigate(["/users/reset-password", id]);
  }

  delete(id) {
    let msg = "Da li ste sigurni da želite izvršiti brisanje?";
    let title = "Upozorenje";
    this._modalService.activate(msg, title).then((responseOK) => {
      if (responseOK) {
        this._usersService.delete(id).subscribe((response) => {
          if (response == null) {
            this._toastService.activate("Uspješno ste obrisali korisnika.");
            this.getUsers();
          }
        });
      }
    });
  }
}
