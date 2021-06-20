import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

import { AboutAuthorDetailsComponent } from "./about-author-details/about-author-details.component";
import { AboutAuthorFormComponent } from "./about-author-form/about-author-form.component";

import { AboutAuthorService } from "./about-author.service";

import { AboutAuthorRoutingModule } from "./about-author-routing.module";

import { FileUploadModule } from "../common/file-upload/file-upload.module";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    AboutAuthorRoutingModule,
    FileUploadModule,
  ],
  declarations: [AboutAuthorDetailsComponent, AboutAuthorFormComponent],
  providers: [AboutAuthorService],
  exports: [AboutAuthorDetailsComponent],
})
export class AboutAuthorModule {}
