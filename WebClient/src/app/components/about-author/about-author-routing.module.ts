import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AboutAuthorDetailsComponent } from "./about-author-details/about-author-details.component";
import { AboutAuthorFormComponent } from "./about-author-form/about-author-form.component";

const pictureRoutes: Routes = [
  {
    path: "about-author",
    component: AboutAuthorDetailsComponent,
    pathMatch: "full",
  },
  {
    path: "about-author/form/:id",
    component: AboutAuthorFormComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(pictureRoutes)],
  exports: [RouterModule],
})
export class AboutAuthorRoutingModule {}
