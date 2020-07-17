import { Component, OnInit } from '@angular/core';
import { UsersService } from "../users.services";
import { Router, ActivatedRoute } from '@angular/router';
import { User } from "../user";
import { ToastService } from "../../common/toast/toast.service";
import { RolesService } from "../../roles/roles.services";
import { ModalService } from "../../common/modal/modal.service";

@Component({
  selector: 'app-users-form',
  templateUrl: './users-form.component.html',
  styleUrls: ['./users-form.component.css']
})
export class UsersFormComponent implements OnInit {

  id: number = 0;
  model: User;
  roles: any[];  
  previousUsername: string = null;

  constructor(private _usersService: UsersService,
              private _route: ActivatedRoute,
              private _router: Router,
              private _toastService: ToastService,
              private _rolesService: RolesService,
              private _modalService: ModalService) {
      this.model = new User();
    }

  ngOnInit() {
    this._route.params.subscribe(params => {
      let id = +params['id'];
      if (!isNaN(id) && id > 0) {   
        this.getUser(id); 
      }
      else {
        this.initModel(); 
      }
      this.getRoles();
    });
  }

  initModel(){
    this.model.id = 0;
    this.model.firstName = null;
    this.model.lastName = null;
    this.model.username = null;
    this.model.password = null;
    this.model.roleId = 0;
  }

  getRoles(){
    this._rolesService.getDropDownItems().subscribe(response =>{
      this.roles = response;  
    });
  }

  getUser(id){
    this._usersService.getById(id).subscribe(response => {
        this.model = response;
        this.previousUsername = this.model.username;
    });
  }

  save(){
    if(this.usernameValidation() && 
    this.passwordValidation() &&
    this.roleValidation()){
      if (this.model.id == 0) {
        this.insert();
      }else{
        this.update();
      }      
    }
  }

  insert(){
    this._usersService.insert(this.model).subscribe(response => {
      this._toastService.activate("Korisnik je uspješno kreirana.", "alert-success");
      this.goBack()      
    },
    error => {
      this._toastService.activate(error.error.exceptionMessage, "alert-danger");
    });
  }

  update(){
    if (!this.compareUsernames()) {
      let msg = "Da li ste sigurni da želite promjeniti korisničko ime?";
      let title = "Upozorenje";
      this._modalService.activate(msg, title).then((responseOK) => {
        if (responseOK) {
          this._usersService.update(this.model).subscribe(response => {       
            this._toastService.activate("Kategorija je uspješno izmjenjena.", "alert-success");
            this.goBack()
          },
          error => {
            this._toastService.activate(error.error.exceptionMessage, "alert-danger");
          });
        }
      });
      
    } else {
      this._usersService.update(this.model).subscribe(response => {       
        this._toastService.activate("Kategorija je uspješno izmjenjena.", "alert-success");
        this.goBack()
      },
      error => {
        this._toastService.activate(error.error.exceptionMessage, "alert-danger");
      });
    }    
  }

  usernameValidation() {
    if (this.model.username == null || this.model.username.length < 1) {
      return false;
    }
    return true;
  }
  passwordValidation() {
    if ((this.model.password == null || this.model.password.length < 1) && this.model.id == 0) {
      return false;
    }
    return true;
  }

  roleValidation() {
    if (this.model.roleId < 1) {
      return false;
    }      
    return true;
  }

  compareUsernames(){
    if(this.previousUsername === this.model.username){
      return true;
    }
    return false;
  }

  goBack() {
    this._router.navigate(['/users']);
  }
}
