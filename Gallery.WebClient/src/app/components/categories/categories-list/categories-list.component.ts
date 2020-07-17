import { Component, OnInit } from '@angular/core';
import { CategoriesService } from "../categories.services";
import { Router } from '@angular/router';
import { Category } from '../category';
import { ModalService } from "../../common/modal/modal.service";
import { ToastService } from "../../common/toast/toast.service";

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.css']
})
export class CategoriesListComponent implements OnInit {

  categories: Category[];
  categoriesToDisplay: Category[];
  categoriesPerPage: number = 5;
  numberOfPages: number[] = [];
  currentPage: number = 0;

  constructor(private _categoriesService: CategoriesService,
              private _router: Router,
              private _modalService: ModalService,
              private _toastService: ToastService) { 
    this.categories = [];
    this.categoriesToDisplay = [];
  }

  ngOnInit() {
    this.getCategories();
  }

  initialize(data) {
    this.categories = data;
    this.numberOfPages.length = Math.ceil(this.categories.length / this.categoriesPerPage);
    if (this.numberOfPages.length > 1) {
        this.numberOfPages = Array.from(Array(this.numberOfPages.length).keys());
    }
    this.changePage(this.currentPage);
  }

  changePage(pageNum) {
    this.currentPage = pageNum;
    this.categoriesToDisplay = this.categories.slice((this.currentPage * this.categoriesPerPage),
                                                    ((this.currentPage * this.categoriesPerPage) + this.categoriesPerPage));
    if (this.categoriesToDisplay.length == 0) {
      this._toastService.activate("Nema podataka za prikaz.", "alert-danger");
    }
  }

  getCategories(){
    this._categoriesService.getAll().subscribe(data => {
        this.initialize(data);
    },
    error => {
      this._toastService.activate(error.error.message, "alert-danger");
    });
  }

  goToCategoryForm(id) {
    this._router.navigate(['/categories/form', id]);
  }

  delete(id){
    let msg = "Da li ste sigurni da želite izvršiti brisanje?";
    let title = "Upozorenje";
    this._modalService.activate(msg, title).then((responseOK) => {
      if (responseOK) {
        this._categoriesService.delete(id).subscribe(response => {
          this._toastService.activate("Uspješno se obrisali kategoriju.");
          this.getCategories();          
        },
        error => {
          this._toastService.activate(error.error.message, "alert-danger");
        });
      }
    });
  }
}
