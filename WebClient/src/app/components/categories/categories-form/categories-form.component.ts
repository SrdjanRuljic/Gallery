import { Component, OnInit } from "@angular/core";
import { CategoriesService } from "../categories.services";
import { Router, ActivatedRoute } from "@angular/router";
import { Category } from "../category";
import { ToastService } from "../../common/toast/toast.service";

@Component({
  selector: "app-categories-form",
  templateUrl: "./categories-form.component.html",
  styleUrls: ["./categories-form.component.css"],
})
export class CategoriesFormComponent implements OnInit {
  id: number = 0;
  model: Category;

  constructor(
    private _categoriesService: CategoriesService,
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastService: ToastService
  ) {
    this.model = new Category();
  }

  ngOnInit() {
    this._route.params.subscribe((params) => {
      let id = +params["id"];
      if (!isNaN(id) && id > 0) {
        this.getCategory(id);
      } else {
        this.initModel();
      }
    });
  }

  initModel() {
    this.model.id = 0;
    this.model.name = null;
  }

  getCategory(id) {
    this._categoriesService.getById(id).subscribe((response) => {
      this.model = response;
    });
  }

  save() {
    if (this.nameValidation()) {
      if (this.model.id == 0) {
        this.insert();
      } else {
        this.update();
      }
    }
  }

  insert() {
    this._categoriesService.insert(this.model).subscribe((response) => {
      this._toastService.activate(
        "Kategorija je uspješno kreirana.",
        "alert-success"
      );
      this.goBack();
    });
  }

  update() {
    this._categoriesService.update(this.model).subscribe((response) => {
      this._toastService.activate(
        "Kategorija je uspješno izmjenjena.",
        "alert-success"
      );
      this.goBack();
    });
  }

  nameValidation() {
    return !!!(this.model.name == null || this.model.name.length < 1);
  }

  goBack() {
    this._router.navigate(["/categories"]);
  }
}
