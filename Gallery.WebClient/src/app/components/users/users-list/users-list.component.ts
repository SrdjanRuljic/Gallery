import { Component, OnInit } from '@angular/core';
import { UsersService } from "../users.services";
import { Router } from '@angular/router';
import { User } from '../user';
import { ModalService } from "../../common/modal/modal.service";
import { ToastService } from "../../common/toast/toast.service";

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {

  users: User[];
  usersToDisplay: User[];
  usersPerPage: number = 5;
  numberOfPages: number[] = [];
  currentPage: number = 0;

  constructor(private _usersService: UsersService,
              private _router: Router,
              private _modalService: ModalService,
              private _toastService: ToastService) { 
    this.users = [];
    this.usersToDisplay = [];
  }

  ngOnInit() {
    this.getUsers();
  }

  initialize(data) {
    this.users = data;
    this.numberOfPages.length = Math.ceil(this.users.length / this.usersPerPage);
    if (this.numberOfPages.length > 1) {
        this.numberOfPages = Array.from(Array(this.numberOfPages.length).keys());
    }
    this.changePage(this.currentPage);
  }

  changePage(pageNum) {
    this.currentPage = pageNum;
    this.usersToDisplay = this.users.slice((this.currentPage * this.usersPerPage),
                                                    ((this.currentPage * this.usersPerPage) + this.usersPerPage));
    if (this.usersToDisplay.length == 0) {
      this._toastService.activate("Nema podataka za prikaz.", "alert-danger");
    }
  }

  getUsers(){
    this._usersService.getAll().subscribe(data => {
        this.initialize(data);
    });
  }

  goToUserForm(id) {
    this._router.navigate(['/users/form', id]);
  }

  delete(id){
    let msg = "Da li ste sigurni da želite izvršiti brisanje?";
    let title = "Upozorenje";
    this._modalService.activate(msg, title).then((responseOK) => {
      if (responseOK) {
        this._usersService.delete(id).subscribe(response => {
          if (response == null) {            
            this._toastService.activate("Uspješno se obrisali korisnika.");
            this.getUsers();
          }
          else {
            this._toastService.activate(response, "alert-danger");
          }
        });
      }
    });
  }
}
