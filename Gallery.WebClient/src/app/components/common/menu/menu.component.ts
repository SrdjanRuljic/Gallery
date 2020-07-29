import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalEventsManager } from "../global-event-manager";

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  isAdmin: boolean = false;
  displayName: string = null;

  constructor(private _router: Router,
              private _globalEventsManager: GlobalEventsManager,) { 
    this.isAuthorized();
    this.getLocalUserData();
  }

  ngOnInit() {
  }

  isAuthorized(){
    const token = localStorage.getItem('auth_token');
    return !!token;
  }

  getLocalUserData(){
    this._globalEventsManager.isAdmin.subscribe((mode: boolean) => {
      this.isAdmin = mode;
    });
    this._globalEventsManager.displayName.subscribe((mode: string) => {
      this.displayName = mode;
    });
  }

  goToAboutAuthor() {
      this._router.navigate(['/about-author']);
  }

  goToContacts() {
      this._router.navigate(['/contacts']);
  }

  goToHome(){
      this._router.navigate(['/']);
  }

  goToCategories(){
    this._router.navigate(['/categories']);
  }

  goToUsers(){
    this._router.navigate(['/users']);
  }
}
