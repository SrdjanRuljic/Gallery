import { Component, OnInit } from "@angular/core";
import { PicturesService } from "../pictures.service";
import { Router } from "@angular/router";
import { ToastService } from "../../common/toast/toast.service";
import { CategoriesService } from "../../categories/categories.services";
import { AuthService } from "../../common/auth/auth.services";
import { Observable } from "rxjs";

export class SearchModel {
  name: string;
  categoryId: number;
}

@Component({
  selector: "app-pictures-list",
  templateUrl: "./pictures-list.component.html",
  styleUrls: ["./pictures-list.component.scss"],
})
export class PicturesListComponent implements OnInit {
  pictures: any[];
  itemsToDisplay: any[];
  numberOfPages = [];
  itemPerPage: number = 12;
  currentPage: number = 0;
  searchModel: SearchModel;
  categories: any[];

  isAuthorized: Observable<boolean>;

  constructor(
    private _picturesService: PicturesService,
    private _router: Router,
    private _toastService: ToastService,
    private _categoriesService: CategoriesService,
    private _authService: AuthService
  ) {
    this.isAuthorized = this._authService.getIsAuthorized();
    this.pictures = [];
    this.itemsToDisplay = [];
    this.searchModel = new SearchModel();
  }

  ngOnInit() {
    this.getCategories();
    this.initSearchModel();
    this.search();
  }

  initSearchModel() {
    this.searchModel.name = null;
    this.searchModel.categoryId = 0;
  }

  search() {
    this._picturesService.search(this.searchModel).subscribe((data) => {
      this.initialize(data);
    });
  }

  resetSearch() {
    this.currentPage = 0;
    this.initSearchModel();
    this.search();
  }

  initialize(data) {
    this.pictures = data;
    this.numberOfPages.length = Math.ceil(
      this.pictures.length / this.itemPerPage
    );
    if (this.numberOfPages.length > 1) {
      this.numberOfPages = Array.from(Array(this.numberOfPages.length).keys());
    }
    this.changePage(this.currentPage);
  }

  changePage(pageNum) {
    this.currentPage = pageNum;
    this.itemsToDisplay = this.pictures.slice(
      this.currentPage * this.itemPerPage,
      this.currentPage * this.itemPerPage + this.itemPerPage
    );
    if (this.itemsToDisplay.length == 0) {
      this._toastService.activate("Nema podataka za prikaz.", "alert-danger");
    }
  }

  getCategories() {
    this._categoriesService.getDropDownItems().subscribe((response) => {
      this.categories = response;
    });
  }

  goToPictureForm(id) {
    this._router.navigate(["/pictures/form", id]);
  }

  goToPictureDetails(id) {
    this._router.navigate(["/pictures/details", id]);
  }
}
