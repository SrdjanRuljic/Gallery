import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../common/toast/toast.service';
import { Contact } from '../contact';
import { ContactsService } from '../contacts.service';

@Component({
  selector: 'app-contacts-form',
  templateUrl: './contacts-form.component.html',
  styleUrls: ['./contacts-form.component.scss']
})
export class ContactsFormComponent implements OnInit {

  id: number = 0;
  model: Contact;

  constructor(private _contactsService: ContactsService,
              private _route: ActivatedRoute,
              private _router: Router,
              private _toastService: ToastService) {
    this.model = new Contact();
  }

  ngOnInit() {
    this._route.params.subscribe((params) => {
      let id = +params["id"];
      if (!isNaN(id) && id > 0) {
        this.getContact(id);
      } else {
        this.initModel();
      }
    });
  }

  initModel() {
    this.model.id = 0;
    this.model.name = null;
    this.model.value = null;
  }

  getContact(id) {
    this._contactsService.getById(id).subscribe(
      (response) => {
        this.model = response;
      },
      (error) => {
        this._toastService.activate(error.error.message, "alert-danger");
      }
    );
  }

  save() {
    if (this.nameValidation() && this.valueValidation()) {
      if (this.model.id == 0) {
        this.insert();
      } else {
        this.update();
      }
    }
  }

  insert() {
    this._contactsService.insert(this.model).subscribe(
      (response) => {
        this._toastService.activate("Kontakt je uspješno kreiran.", "alert-success");
        this.goBack();
      },
      (error) => {
        this._toastService.activate(error.error.message, "alert-danger");
      }
    );
  }

  update() {
    this._contactsService.update(this.model).subscribe(
      (response) => {
        this._toastService.activate("Kontakt je uspješno izmjenjen.",  "alert-success");
        this.goBack();
      },
      (error) => {
        this._toastService.activate(error.error.message, "alert-danger");
      }
    );
  }

  nameValidation() {
    if (this.model.name == null || this.model.name.length < 1) {
      return false;
    }
    return true;
  }

  valueValidation() {
    if (this.model.value == null || this.model.value.length < 1) {
      return false;
    }
    return true;
  }

  goBack() {
    this._router.navigate(["/contacts"]);
  }
}
