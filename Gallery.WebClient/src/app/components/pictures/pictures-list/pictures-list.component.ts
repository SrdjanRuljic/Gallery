import { Component, OnInit } from "@angular/core";
import { PicturesService } from "../pictures.service";
import { Router } from "@angular/router";
import { ToastService } from "../../common/toast/toast.service";

@Component({
  selector: "app-pictures-list",
  templateUrl: "./pictures-list.component.html",
  styleUrls: ["./pictures-list.component.scss"],
})
export class PicturesListComponent implements OnInit {
  pictures: any[];
  rows = [];

  constructor(
    private _picturesService: PicturesService,
    private _router: Router,
    private _toastService: ToastService
  ) {
    this.pictures = [];
  }

  ngOnInit() {
    this.pictures = this._picturesService.getImages();
    this.rows = Array.from(Array(Math.ceil(this.pictures.length / 4)).keys());
    this.search();
  }

  search() {
    this._picturesService.search().subscribe(
      (data) => {
        console.log(data);
      },
      (error) => {
        this._toastService.activate(error.error.message, "alert-danger");
      }
    );
  }

  goToPictureForm(id) {
    this._router.navigate(["/pictures/form", id]);
  }

  goToPictureDetails(id) {
    this._router.navigate(["/pictures/details", id]);
  }
}
