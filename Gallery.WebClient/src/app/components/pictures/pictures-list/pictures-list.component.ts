import { Component, OnInit } from '@angular/core';
import { PicturesService } from "../pictures.service";
import { Router } from '@angular/router';

@Component({
  selector: 'app-pictures-list',
  templateUrl: './pictures-list.component.html',
  styleUrls: ['./pictures-list.component.scss']
})
export class PicturesListComponent implements OnInit {

  pictures: any[];
  rows = [];

  constructor(private _picturesService: PicturesService,
              private _router: Router) {
    this.pictures = [];
   }

  ngOnInit() {    
    this.pictures = this._picturesService.getImages();
    this.rows = Array.from(Array(Math.ceil(this.pictures.length / 4)).keys());
  }

  goToPictureForm(id) {
    this._router.navigate(['/pictures/form', id]);
  }

  goToPictureDetails(id){
    this._router.navigate(['/pictures/details', id]);
  }
}
