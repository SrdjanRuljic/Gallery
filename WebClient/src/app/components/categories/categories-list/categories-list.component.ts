import { Component, OnInit } from "@angular/core";
import { CategoriesService } from "../categories.services";
import { Router } from "@angular/router";
import { Category } from "../category";
import { ModalService } from "../../common/modal/modal.service";
import { ToastService } from "../../common/toast/toast.service";
import { AuthService } from "../../common/auth/auth.services";
import { Observable } from "rxjs";
import { Pagination, SearchModel } from "../../common/pagination";

@Component({
  selector: "app-categories-list",
  templateUrl: "./categories-list.component.html",
  styleUrls: ["./categories-list.component.css"],
})
export class CategoriesListComponent implements OnInit {
  searchModel: SearchModel;
  pagination: Pagination;
  categories: Category[];

  isAuthorized: Observable<boolean>;

  constructor(
    private _categoriesService: CategoriesService,
    private _router: Router,
    private _modalService: ModalService,
    private _toastService: ToastService,
    private _authService: AuthService
  ) {
    this.searchModel = new SearchModel();
    this.pagination = new Pagination();
    this.isAuthorized = this._authService.getIsAuthorized();
    this.categories = [];
  }

  ngOnInit() {
    this.initSearchModel();
    this.getCategories();
  }

  initSearchModel() {
    this.searchModel.pageNumber = 1;
    this.searchModel.pageSize = 10;
  }

  pageChanged(event: any) {
    this.searchModel.pageNumber = event.page;
    this.getCategories();
  }

  getCategories() {
    this._categoriesService.getAll(this.searchModel).subscribe((response) => {
      this.categories = response.list;
      this.pagination.pageNumber = response.pageNumber;
      this.pagination.totalCount = response.totalCount;
      this.pagination.totalPages = response.totalPages;
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
