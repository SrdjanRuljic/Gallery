import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AboutAuthorComponent } from "./components/about-author/about-author.component";

const routes: Routes = [
  { path: "", redirectTo: "pictures", pathMatch: "full" },
  { path: "about-author", component: AboutAuthorComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
