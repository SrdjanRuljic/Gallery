import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AboutAuthorComponent } from "../about-author/about-author.component";

const pictureRoutes: Routes = [
  {
    path: "about-author",
    component: AboutAuthorComponent,
    pathMatch: "full",
  },
];

@NgModule({
  imports: [RouterModule.forChild(pictureRoutes)],
  exports: [RouterModule],
})
export class AboutAuthorRoutingModule {}
