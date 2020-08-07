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
  itemsToDisplay: any[];
  numberOfPages = [];
  itemPerPage: number = 12;
  currentPage: number = 0;

  constructor(
    private _picturesService: PicturesService,
    private _router: Router,
    private _toastService: ToastService
  ) {
    this.pictures = [];
  }

  ngOnInit() {
    this.search();
  }

  search() {
    this._picturesService.search().subscribe(
      (data) => {
        this.initialize(data);
      },
      (error) => {
        this._toastService.activate(error.error.message, "alert-danger");
      }
    );
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

  goToPictureForm(id) {
    this._router.navigate(["/pictures/form", id]);
  }

  goToPictureDetails(id) {
    this._router.navigate(["/pictures/details", id]);
  }
}
