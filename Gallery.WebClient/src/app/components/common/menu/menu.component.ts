import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalEventsManager } from "../global-event-manager";
import { AuthService } from "../auth/auth.services";
import { UsersService } from "../../users/users.services";
import { ToastService } from "../toast/toast.service";

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  isAuthorized: boolean = false;
  isAdmin: boolean = false;
  displayName: string = null;

  constructor(private _router: Router,
              private _globalEventsManager: GlobalEventsManager,
              private _authService: AuthService,
              private _usersService: UsersService,
              private _toastService: ToastService) { 
    this.ifAuthorized();
    this.getLocalUserData();
  }

  ngOnInit() {
    this._globalEventsManager.isAuthorized.emit(this._authService.isLoggedIn);
    if(this.isAuthorized){
      this.getLogedInUserData();
    }
  }

  ifAuthorized(){
    this._globalEventsManager.isAuthorized.subscribe((mode: boolean) => {
      this.isAuthorized = mode;
    });  
  }

  getLocalUserData(){
    this._globalEventsManager.isAdmin.subscribe((mode: boolean) => {
      this.isAdmin = mode;
    });
    this._globalEventsManager.displayName.subscribe((mode: string) => {
      this.displayName = mode;
    });
  }

  getLogedInUserData(){
    this._usersService.getLogedInUserData().subscribe(response =>{
      this._globalEventsManager.isAdmin.emit(response.isAdmin);     
      this._globalEventsManager.displayName.emit(response.displayName);  
    },error => {
      this._toastService.activate(error.error.message, "alert-danger");
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
