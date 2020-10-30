import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

import { AboutAuthorComponent } from "./about-author.component";

import { AboutAuthorService } from "./about-author.service";

import { AboutAuthorRoutingModule } from "./about-author-routing.module";

@NgModule({
  imports: [CommonModule, FormsModule, AboutAuthorRoutingModule],
  declarations: [AboutAuthorComponent],
  providers: [AboutAuthorService],
  exports: [AboutAuthorComponent],
})
export class AboutAuthorModule {}
