import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

import { UsersListComponent } from "./users-list/users-list.component";
import { UsersFormComponent } from "./users-form/users-form.component";
import { UsersResetPasswordComponent } from "./users-reset-password/users-reset-password.component";

import { UsersService } from "./users.services";

import { UsersRoutingModule } from "./users-routing.module";

@NgModule({
  imports: [CommonModule, FormsModule, UsersRoutingModule],
  declarations: [
    UsersListComponent,
    UsersFormComponent,
    UsersResetPasswordComponent,
  ],
  providers: [UsersService],
  exports: [UsersRoutingModule],
})
export class UsersModule {}
