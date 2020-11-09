import { Component, OnInit } from "@angular/core";
import { PicturesService } from "../pictures.service";
import { Router, ActivatedRoute } from "@angular/router";
import { ToastService } from "../../common/toast/toast.service";
import { ModalService } from "../../common/modal/modal.service";
import { Picture } from "../picture";
import { AuthService } from "../../common/auth/auth.services";

@Component({
  selector: "app-pictures-details",
  templateUrl: "./pictures-details.component.html",
  styleUrls: ["./pictures-details.component.scss"],
})
export class PicturesDetailsComponent implements OnInit {
  model: Picture;

  constructor(
    private _picturesService: PicturesService,
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastService: ToastService,
    private _modalService: ModalService,
    private _authService: AuthService
  ) {
    this.model = new Picture();
  }

  ngOnInit() {
    this._route.params.subscribe((params) => {
      let id = +params["id"];
      this.getPicture(id);
    });
  }

  getPicture(id) {
    this._picturesService.getById(id).subscribe((response) => {
      this.model = response;
    });
  }

  goToPictureForm(id) {
    this._router.navigate(["/pictures/form", id]);
  }

  goToPictureList() {
    this._router.navigate(["/pictures"]);
  }

  isAuthorized() {
    return this._authService.isAuthorized();
  }

  delete(id) {
    let msg = "Da li ste sigurni da želite izvršiti brisanje?";
    let title = "Upozorenje";
    this._modalService.activate(msg, title).then((responseOK) => {
      if (responseOK) {
        this._picturesService.delete(id).subscribe((response) => {
          if (response == null) {
            this._toastService.activate("Uspješno se obrisali sliku.");
            this.goToPictureList();
          }
        });
      }
    });
  }
}
