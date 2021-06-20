import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

import { ContactsListComponent } from "./contacts-list/contacts-list.component";
import { ContactsFormComponent } from "./contacts-form/contacts-form.component";

import { ContactsService } from "./contacts.service";

import { ContactsRoutingModule } from "./contacts-routing.module";

@NgModule({
  imports: [CommonModule, FormsModule, ContactsRoutingModule],
  declarations: [ContactsListComponent, ContactsFormComponent],
  providers: [ContactsService],
  exports: [ContactsRoutingModule],
})
export class ContactsModule {}
