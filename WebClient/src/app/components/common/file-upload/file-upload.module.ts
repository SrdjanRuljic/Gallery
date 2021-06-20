import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FileUploadComponent } from "./file-upload.component";

@NgModule({
  imports: [CommonModule],
  exports: [FileUploadComponent],
  declarations: [FileUploadComponent],
  providers: [],
})
export class FileUploadModule {}
