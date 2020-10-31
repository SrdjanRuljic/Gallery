import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { AboutAuthorService } from "../about-author.service";
import { Author } from "../author";

@Component({
  selector: "app-about-author-details",
  templateUrl: "./about-author-details.component.html",
  styleUrls: ["./about-author-details.component.css"],
})
export class AboutAuthorDetailsComponent implements OnInit {
  model: Author;

  constructor(
    private _aboutAuthorService: AboutAuthorService,
    private _route: ActivatedRoute,
    private _router: Router
  ) {
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

  isAuthorized() {
    const token = localStorage.getItem("auth_token");
    return !!token;
  }

  goToAboutAuthorForm(id) {
    this._router.navigate(["/about-author/form", id]);
  }
}
