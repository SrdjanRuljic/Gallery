import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { ContactsListComponent } from "../contacts/contacts-list/contacts-list.component";
import { ContactsFormComponent } from "../contacts/contacts-form/contacts-form.component";

const contactRoutes: Routes = [
  {
    path: "contacts",
    component: ContactsListComponent,
    pathMatch: "full",
  },
  {
    path: "contacts/form/:id",
    component: ContactsFormComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(contactRoutes)],
  exports: [RouterModule],
})
export class ContactsRoutingModule {}
