import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { UsersListComponent } from "./users-list/users-list.component";
import { UsersFormComponent } from "./users-form/users-form.component";
import { UsersUpdatePasswordComponent } from "./users-update-password/users-update-password.component";

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
    path: "users/update-password/:id",
    component: UsersUpdatePasswordComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(userRoutes)],
  exports: [RouterModule],
})
export class UsersRoutingModule {}
