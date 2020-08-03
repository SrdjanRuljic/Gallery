import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

import { PicturesListComponent } from "./pictures-list/pictures-list.component";
import { PicturesDetailsComponent } from "./pictures-details/pictures-details.component";
import { PicturesFormComponent } from "./pictures-form/pictures-form.component";

import { PicturesService } from "./pictures.service";

import { PicturesRoutingModule } from "./pictures-routing.module";
import { FileUploadComponent } from "../common/file-upload/file-upload.component";

@NgModule({
  imports: [CommonModule, FormsModule, PicturesRoutingModule],
  declarations: [
    PicturesListComponent,
    PicturesDetailsComponent,
    PicturesFormComponent,
    FileUploadComponent,
  ],
  providers: [PicturesService],
  exports: [PicturesListComponent],
})
export class PicturesModule {}
