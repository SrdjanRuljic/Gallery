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
  fileData: File;

  constructor(
    private _picturesService: PicturesService,
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastService: ToastService,
    private _categoriesService: CategoriesService
  ) {
    this.model = new Picture();
  }

  ngOnInit() {
    this.getCategories();

    this._route.params.subscribe((params) => {
      let id = params["id"];
      if (!isNaN(id) && id > 0) {
        this.getPicture(id);
      } else {
        this.initModel();
      }
    });
  }

  initModel() {
    this.model.id = 0;
    this.model.name = null;
    this.model.description = null;
    this.model.categoryId = 0;
    this.model.extension = null;
    this.model.content = null;
  }

  getPicture(id) {
    this._picturesService.getById(id).subscribe((response) => {
      this.model = response;
    });
  }

  save() {
    if (this.nameValidation() && this.categoryValidation()) {
      if (this.model.id == 0) {
        this.insert();
      } else {
        this.update();
      }
    }
  }

  insert() {
    this._picturesService.insert(this.model).subscribe((response) => {
      this._toastService.activate("Slika je uspješno dodata.", "alert-success");
      this.goBack();
    });
  }

  update() {
    this._picturesService.update(this.model).subscribe((response) => {
      this._toastService.activate(
        "Slika je uspješno izmjenjena.",
        "alert-success"
      );
      this.goBack();
    });
  }

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

  getFileData(data) {
    this.model.extension = data.extension;
    this.model.content = data.content;
  }

  goBack() {
    this._router.navigate(["/pictures"]);
  }
}
