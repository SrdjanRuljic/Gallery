import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

import { AboutAuthorComponent } from "./about-author.component";

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
  declarations: [AboutAuthorComponent],
  providers: [AboutAuthorService],
  exports: [AboutAuthorComponent],
})
export class AboutAuthorModule {}
