import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { ToastService } from "../../common/toast/toast.service";
import { AboutAuthorService } from "../about-author.service";
import { Author } from "../author";

@Component({
  selector: "app-about-author-form",
  templateUrl: "./about-author-form.component.html",
  styleUrls: ["./about-author-form.component.css"],
})
export class AboutAuthorFormComponent implements OnInit {
  model: Author;

  constructor(
    private _aboutAuthorService: AboutAuthorService,
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastService: ToastService
  ) {
    this.model = new Author();
  }

  ngOnInit() {
    this._route.params.subscribe((params) => {
      let id = params["id"];
      if (!isNaN(id) && id > 0) {
        this.getAuthor(id);
      } else {
        this.initModel();
      }
    });
  }

  initModel() {
    this.model.id = 0;
    this.model.name = null;
    this.model.biography = null;
    this.model.extension = null;
    this.model.content = null;
  }

  getAuthor(id) {
    this._aboutAuthorService.getById(id).subscribe((response) => {
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
    this._aboutAuthorService.insert(this.model).subscribe((response) => {
      this._toastService.activate(
        "Podaci o autoru su uspješno dodati.",
        "alert-success"
      );
      this.goBack();
    });
  }

  update() {
    this._aboutAuthorService.update(this.model).subscribe((response) => {
      this._toastService.activate(
        "Podaci o autoru su uspješno izmjenjeni.",
        "alert-success"
      );
      this.goBack();
    });
  }

  nameValidation() {
    return !!!(this.model.name == null || this.model.name.length < 1);
  }

  getFileData(data) {
    this.model.extension = data.extension;
    this.model.content = data.content;
  }

  goBack() {
    this._router.navigate(["/about-author"]);
  }
}
