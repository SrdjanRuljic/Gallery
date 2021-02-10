import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ModalService } from "../../common/modal/modal.service";
import { ToastService } from "../../common/toast/toast.service";
import { ContactsService } from "../contacts.service";
import { Contact } from "../contact";
import { AuthService } from "../../common/auth/auth.services";

@Component({
  selector: "app-contacts-list",
  templateUrl: "./contacts-list.component.html",
  styleUrls: ["./contacts-list.component.scss"],
})
export class ContactsListComponent implements OnInit {
  contacts: Contact[];
  itemsToDisplay: Contact[];
  itemPerPage: number = 10;
  numberOfPages: number[] = [];
  currentPage: number = 0;

  constructor(
    private _contactsService: ContactsService,
    private _router: Router,
    private _modalService: ModalService,
    private _toastService: ToastService,
    private _authService: AuthService
  ) {
    this.contacts = [];
    this.itemsToDisplay = [];
  }

  ngOnInit() {
    this.getContacts();
  }

  initialize(data) {
    this.contacts = data;
    this.numberOfPages.length = Math.ceil(
      this.contacts.length / this.itemPerPage
    );
    if (this.numberOfPages.length > 1) {
      this.numberOfPages = Array.from(Array(this.numberOfPages.length).keys());
    }
    this.changePage(this.currentPage);
  }

  changePage(pageNum) {
    this.currentPage = pageNum;
    this.itemsToDisplay = this.contacts.slice(
      this.currentPage * this.itemPerPage,
      this.currentPage * this.itemPerPage + this.itemPerPage
    );
    if (this.itemsToDisplay.length == 0) {
      this._toastService.activate("Nema podataka za prikaz.", "alert-danger");
    }
  }

  getContacts() {
    this._contactsService.getAll().subscribe(
      (data) => {
        this.initialize(data);
      },
      (error) => {
        this._toastService.activate(error.error.message, "alert-danger");
      }
    );
  }

  isAuthorized() {
    return this._authService.isAuthorized();
  }

  goToContactForm(id) {
    this._router.navigate(["/contacts/form", id]);
  }

  delete(id) {
    let msg = "Da li ste sigurni da želite izvršiti brisanje?";
    let title = "Upozorenje";
    this._modalService.activate(msg, title).then((responseOK) => {
      if (responseOK) {
        this._contactsService.delete(id).subscribe(
          (response) => {
            this._toastService.activate("Uspješno se obrisali kontakt.");
            this.getContacts();
          },
          (error) => {
            this._toastService.activate(error.error.message, "alert-danger");
          }
        );
      }
    });
  }
}
