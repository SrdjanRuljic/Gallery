import { Component, OnInit, EventEmitter, Output } from "@angular/core";

@Component({
  selector: "app-file-upload",
  templateUrl: "./file-upload.component.html",
  styleUrls: ["./file-upload.component.css"],
})
export class FileUploadComponent implements OnInit {
  imageUrl: string = "/assets/images/User.jfif";
  uploadedFile: File = null;

  @Output() fileData: EventEmitter<File> = new EventEmitter<File>();

  constructor() {}

  ngOnInit() {}

  onFileSelected(files: FileList) {
    this.uploadedFile = files.item(0);

    var reader = new FileReader();

    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
    };
    reader.readAsDataURL(this.uploadedFile);

    this.fileData.emit(this.uploadedFile);
  }
}
