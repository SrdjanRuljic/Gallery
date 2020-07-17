import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }    from '@angular/forms';

import { RolesListComponent } from './roles-list/roles-list.component';

import { RolesService } from "./roles.services";

import { RolesRoutingModule } from "./roles-routing.module";

@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      RolesRoutingModule
    ],
    declarations: [
        RolesListComponent
    ],
    providers: [RolesService],
    exports: [RolesRoutingModule]
  })
  export class RolesModule {}