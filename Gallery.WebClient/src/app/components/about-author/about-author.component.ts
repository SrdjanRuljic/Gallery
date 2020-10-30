import { Component, OnInit } from "@angular/core";
import { AboutAuthorService } from "./about-author.service";
import { Author } from "./author";

@Component({
  selector: "app-about-author",
  templateUrl: "./about-author.component.html",
  styleUrls: ["./about-author.component.css"],
})
export class AboutAuthorComponent implements OnInit {
  model: Author;

  constructor(private _aboutAuthorService: AboutAuthorService) {
    this.model = new Author();
  }

  ngOnInit() {
    this.getAuthor(1);
  }

  getAuthor(id) {
    this._aboutAuthorService.getById(id).subscribe((response) => {
      this.model = response;
    });
  }
}
