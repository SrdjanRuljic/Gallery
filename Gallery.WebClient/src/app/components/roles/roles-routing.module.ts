import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { RolesListComponent } from "./roles-list/roles-list.component";

const roleRoutes: Routes = [
    { 
      path: 'roles', 
      component: RolesListComponent,
      pathMatch: 'full'   
    }
  ];

  @NgModule({
    imports: [
      RouterModule.forChild(roleRoutes)
    ],
    exports: [
      RouterModule
    ]
  })
  export class RolesRoutingModule { }