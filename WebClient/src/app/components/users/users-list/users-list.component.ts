import { Component, OnInit } from "@angular/core";
import { UsersService } from "../users.services";
import { Router } from "@angular/router";
import { User } from "../user";
import { ModalService } from "../../common/modal/modal.service";
import { ToastService } from "../../common/toast/toast.service";
import { Pagination, SearchModel } from "../../common/pagination";

@Component({
  selector: "app-users-list",
  templateUrl: "./users-list.component.html",
  styleUrls: ["./users-list.component.css"],
})
export class UsersListComponent implements OnInit {
  searchModel: SearchModel;
  pagination: Pagination;
  users: User[];

  constructor(
    private _usersService: UsersService,
    private _router: Router,
    private _modalService: ModalService,
    private _toastService: ToastService
  ) {
    this.searchModel = new SearchModel();
    this.pagination = new Pagination();
    this.users = [];
  }

  ngOnInit() {
    this.initSearchModel();
    this.getUsers();
  }

  initSearchModel() {
    this.searchModel.pageNumber = 1;
    this.searchModel.pageSize = 10;
  }

  pageChanged(event: any) {
    this.searchModel.pageNumber = event.page;
    this.getUsers();
  }

  getUsers() {
    this._usersService.getAll(this.searchModel).subscribe((response) => {
      this.users = response.list;
      this.pagination.pageNumber = response.pageNumber;
      this.pagination.totalCount = response.totalCount;
      this.pagination.totalPages = response.totalPages;
    });
  }

  goToUserForm(id) {
    this._router.navigate(["/users/form", id]);
  }

  goToResetPassword(id) {
    this._router.navigate(["/users/update-password", id]);
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
