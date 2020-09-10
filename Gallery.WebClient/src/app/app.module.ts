import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { MyGlobals } from "../my-globals";
import { GlobalEventsManager } from "./components/common/global-event-manager";

import { AppComponent } from "./app.component";
import { AppRoutingModule } from "./app-routing.module";

import { PicturesModule } from "./components/pictures/pictures.module";
import { MenuComponent } from "./components/common/menu/menu.component";
import { AboutAuthorComponent } from "./components/about-author/about-author.component";
import { CategoriesModule } from "./components/categories/categories.module";
import { AuthModule } from "./components/common/auth/auth.module";
import { ToastModule } from "./components/common/toast/toast.module";
import { ModalModule } from "./components/common/modal/modal.module";
import { RolesModule } from "./components/roles/roles.module";
import { UsersModule } from "./components/users/users.module";
import { ContactsModule } from "./components/contacts/contacts.module";

@NgModule({
  declarations: [AppComponent, MenuComponent, AboutAuthorComponent],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    PicturesModule,
    CategoriesModule,
    AuthModule,
    ToastModule,
    ModalModule,
    RolesModule,
    UsersModule,
    ContactsModule,
  ],
  exports: [ToastModule, ModalModule],
  providers: [MyGlobals, GlobalEventsManager],
  bootstrap: [AppComponent],
})
export class AppModule {}
