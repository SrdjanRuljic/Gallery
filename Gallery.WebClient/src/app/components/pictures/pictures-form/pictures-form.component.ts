import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { Picture } from "../picture";
import { PicturesService } from "../pictures.service";
import { ToastService } from "../../common/toast/toast.service";
import { CategoriesService } from "../../categories/categories.services";

@Component({
  selector: "app-pictures-form",
  templateUrl: "./pictures-form.component.html",
  styleUrls: ["./pictures-form.component.scss"],
})
export class PicturesFormComponent implements OnInit {
  model: Picture;
  categories: any[];

  constructor(
    private _picturesServices: PicturesService,
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastService: ToastService,
    private _categoriesService: CategoriesService
  ) {
    this.model = new Picture();
  }

  ngOnInit() {
    this._route.params.subscribe((params) => {
      let id = +params["id"];
      if (!isNaN(id) && id > 0) {
        // this.getUser(id);
      } else {
        this.initModel();
      }
      this.getCategories();
    });
  }

  initModel() {
    this.model.id = 0;
    this.model.name = null;
    this.model.description = null;
    this.model.extension = null;
    this.model.content = null;
    this.model.categoryId = 0;
  }

  save() {}

  nameValidation() {
    return !!!(this.model.name == null || this.model.name.length < 1);
  }

  categoryValidation() {
    return !!!(this.model.categoryId < 1);
  }

  getCategories() {
    this._categoriesService.getDropDownItems().subscribe((response) => {
      this.categories = response;
    });
  }

  goBack() {
    this._router.navigate(["/pictures"]);
  }
}
