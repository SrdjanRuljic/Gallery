import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }    from '@angular/forms';

import { CategoriesListComponent } from './categories-list/categories-list.component';
import { CategoriesFormComponent } from "./categories-form/categories-form.component";

import { CategoriesService } from "./categories.services";

import { CategoriesRoutingModule } from "./categories-routing.module";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CategoriesRoutingModule
  ],
  declarations: [
    CategoriesListComponent,
    CategoriesFormComponent
  ],
  providers: [CategoriesService],
  exports: [CategoriesRoutingModule]
})
export class CategoriesModule {}