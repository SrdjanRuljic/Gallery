import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PicturesListComponent } from "../pictures/pictures-list/pictures-list.component";
import { PicturesFormComponent } from "../pictures/pictures-form/pictures-form.component";
import { PicturesDetailsComponent } from "../pictures/pictures-details/pictures-details.component";

const pictureRoutes: Routes = [
  { 
    path: 'pictures', 
    component: PicturesListComponent,
    pathMatch: 'full'   
  },
  { 
    path: 'pictures/form/:id?', 
    component: PicturesFormComponent 
  },
  { 
    path: 'pictures/details/:id', 
    component: PicturesDetailsComponent 
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(pictureRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class PicturesRoutingModule { }