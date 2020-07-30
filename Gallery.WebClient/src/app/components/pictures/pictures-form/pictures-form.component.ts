import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { Picture } from "../picture";
import { PicturesService } from "../pictures.service";
import { ToastService } from "../../common/toast/toast.service";

@Component({
  selector: "app-pictures-form",
  templateUrl: "./pictures-form.component.html",
  styleUrls: ["./pictures-form.component.scss"],
})
export class PicturesFormComponent implements OnInit {
  model: Picture;

  constructor(
    private _picturesServices: PicturesService,
    private _router: Router,
    private _toastService: ToastService
  ) {
    this.model = new Picture();
  }

  ngOnInit() {}

  save() {}

  nameValidation() {
    if (this.model.name == null || this.model.name.length < 1) {
      return false;
    }
    return true;
  }

  goBack() {
    this._router.navigate(["/pictures"]);
  }
}
