import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CategoriesListComponent } from "../categories/categories-list/categories-list.component";
import { CategoriesFormComponent } from "../categories/categories-form/categories-form.component";

const categoryRoutes: Routes = [
  { 
    path: 'categories', 
    component: CategoriesListComponent,
    pathMatch: 'full'   
  },
  { 
    path: 'categories/form/:id', 
    component: CategoriesFormComponent 
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(categoryRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class CategoriesRoutingModule { }