import { Component, OnInit } from "@angular/core";
import { CategoriesService } from "../categories.services";
import { Router } from "@angular/router";
import { Category } from "../category";
import { ModalService } from "../../common/modal/modal.service";
import { ToastService } from "../../common/toast/toast.service";
import { AuthService } from "../../common/auth/auth.services";
import { Observable } from "rxjs";

@Component({
  selector: "app-categories-list",
  templateUrl: "./categories-list.component.html",
  styleUrls: ["./categories-list.component.css"],
})
export class CategoriesListComponent implements OnInit {
  categories: Category[];
  itemsToDisplay: Category[];
  itemPerPage: number = 10;
  numberOfPages: number[] = [];
  currentPage: number = 0;

  isAuthorized: Observable<boolean>;

  constructor(
    private _categoriesService: CategoriesService,
    private _router: Router,
    private _modalService: ModalService,
    private _toastService: ToastService,
    private _authService: AuthService
  ) {
    this.isAuthorized = this._authService.getIsAuthorized();
    this.categories = [];
    this.itemsToDisplay = [];
  }

  ngOnInit() {
    this.getCategories();
  }

  initialize(data) {
    this.categories = data;
    this.numberOfPages.length = Math.ceil(
      this.categories.length / this.itemPerPage
    );
    if (this.numberOfPages.length > 1) {
      this.numberOfPages = Array.from(Array(this.numberOfPages.length).keys());
    }
    this.changePage(this.currentPage);
  }

  changePage(pageNum) {
    this.currentPage = pageNum;
    this.itemsToDisplay = this.categories.slice(
      this.currentPage * this.itemPerPage,
      this.currentPage * this.itemPerPage + this.itemPerPage
    );
    if (this.itemsToDisplay.length == 0) {
      this._toastService.activate("Nema podataka za prikaz.", "alert-danger");
    }
  }

  getCategories() {
    this._categoriesService.getAll().subscribe((data) => {
      this.initialize(data);
    });
  }

  goToCategoryForm(id) {
    this._router.navigate(["/categories/form", id]);
  }

  delete(id) {
    let msg = "Da li ste sigurni da želite izvršiti brisanje?";
    let title = "Upozorenje";
    this._modalService.activate(msg, title).then((responseOK) => {
      if (responseOK) {
        this._categoriesService.delete(id).subscribe((response) => {
          this._toastService.activate("Uspješno se obrisali kategoriju.");
          this.getCategories();
        });
      }
    });
  }
}
