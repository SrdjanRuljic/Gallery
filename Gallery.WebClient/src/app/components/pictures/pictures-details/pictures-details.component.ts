import { Component, OnInit } from '@angular/core';
import { PicturesService } from '../pictures.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastService } from '../../common/toast/toast.service';
import { ModalService } from '../../common/modal/modal.service';
import { Picture } from '../picture';

@Component({
  selector: 'app-pictures-details',
  templateUrl: './pictures-details.component.html',
  styleUrls: ['./pictures-details.component.scss']
})
export class PicturesDetailsComponent implements OnInit {

  model: Picture;

  constructor(private _picturesService: PicturesService,
              private _route: ActivatedRoute,
              private _router: Router,
              private _toastService: ToastService,
              private _modalService: ModalService) { 
      this.model = new Picture();
    }

  ngOnInit() {
    this._route.params.subscribe(params => {
      let id = +params['id']; 
      this.getPicture(id);       
    });
  }

  getPicture(id){
    this._picturesService.getById(id).subscribe(response => {
        this.model = response;
    },
    error => {
      this._toastService.activate(error.error.message, "alert-danger");
    });
  }

  goToPictureForm(id){
    this._router.navigate(["/pictures/form", id]);
  }

  goToPictureList() {
    this._router.navigate(['/pictures']);
  }

  isAuthorized() {
    const token = localStorage.getItem("auth_token");
    return !!token;
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
          } else {
            this._toastService.activate(response, "alert-danger");
          }
        });
      }
    });
  }
}
