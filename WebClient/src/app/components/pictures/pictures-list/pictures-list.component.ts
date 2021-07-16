import { Component, OnInit } from "@angular/core";
import { PicturesService } from "../pictures.service";
import { Router } from "@angular/router";
import { ToastService } from "../../common/toast/toast.service";
import { CategoriesService } from "../../categories/categories.services";
import { AuthService } from "../../common/auth/auth.services";
import { Observable } from "rxjs";
import { Pagination } from "../../common/pagination";

export class SearchModel {
  name: string;
  categoryId: number;
  pageNumber: number;
  pageSize: number;
}

@Component({
  selector: "app-pictures-list",
  templateUrl: "./pictures-list.component.html",
  styleUrls: ["./pictures-list.component.scss"],
})
export class PicturesListComponent implements OnInit {
  pictures: any[];
  searchModel: SearchModel;
  pagination: Pagination;
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
    this.searchModel = new SearchModel();
    this.pagination = new Pagination();
  }

  ngOnInit() {
    this.getCategories();
    this.initSearchModel();
    this.search();
  }

  initSearchModel() {
    this.searchModel.name = null;
    this.searchModel.categoryId = 0;
    this.searchModel.pageNumber = 1;
    this.searchModel.pageSize = 12;
  }

  search() {
    this._picturesService.search(this.searchModel).subscribe((response) => {
      this.pictures = response.list;
      this.pagination.pageNumber = response.pageNumber;
      this.pagination.totalCount = response.totalCount;
      this.pagination.totalPages = response.totalPages;
    });
  }

  resetSearch() {
    this.initSearchModel();
    this.search();
  }

  pageChanged(event: any) {
    this.searchModel.pageNumber = event.page;
    this.search();
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
