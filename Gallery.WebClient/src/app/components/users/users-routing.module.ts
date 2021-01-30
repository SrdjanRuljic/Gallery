import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { UsersListComponent } from "./users-list/users-list.component";
import { UsersFormComponent } from "./users-form/users-form.component";
import { UsersResetPasswordComponent } from "./users-reset-password/users-reset-password.component";

const userRoutes: Routes = [
  {
    path: "users",
    component: UsersListComponent,
    pathMatch: "full",
  },
  {
    path: "users/form/:id",
    component: UsersFormComponent,
  },
  {
    path: "users/reset-password/:id",
    component: UsersResetPasswordComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(userRoutes)],
  exports: [RouterModule],
})
export class UsersRoutingModule {}
