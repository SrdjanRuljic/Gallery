import { Component, OnInit, EventEmitter, Output } from "@angular/core";
import { FileData } from "./file";

@Component({
  selector: "app-file-upload",
  templateUrl: "./file-upload.component.html",
  styleUrls: ["./file-upload.component.css"],
})
export class FileUploadComponent implements OnInit {
  imageUrl: string = "/assets/images/no-image.png";
  uploadedFile: File = null;
  dataToEmit: FileData;

  @Output() fileData: EventEmitter<FileData> = new EventEmitter<FileData>();

  constructor() {
    this.dataToEmit = new FileData();
  }

  ngOnInit() {}

  onFileSelected(files: FileList) {
    this.uploadedFile = files.item(0);

    this.dataToEmit.extension = this.uploadedFile.name
      .toString()
      .replace(/^.*?\./, "");

    this.readFilesOnInsert(this.uploadedFile);
  }

  readFilesOnInsert(file: any) {
    let reader = new FileReader();

    this.readFile(file, reader, (result: any) => {
      var img = document.createElement("img");
      img.src = result;

      this.imageUrl = result;

      this.dataToEmit.content = result;
    });

    setTimeout(() => {
      this.fileData.emit(this.dataToEmit);
    }, 1000);
  }

  readFile(file: File, reader: any, callback: any) {
    reader.onload = () => {
      callback(reader.result);
      this.imageUrl = reader.result;
    };
    reader.readAsDataURL(file);
  }
}
