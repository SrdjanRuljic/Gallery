import { Component, OnInit, OnDestroy, Input, ViewEncapsulation } from '@angular/core';
import { ModalService } from './modal.service';
import { Subscription } from 'rxjs';

const KEY_ESC = 27;

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css'],
  encapsulation: ViewEncapsulation.None
})

export class ModalComponent implements OnInit, OnDestroy {
    @Input() name;
    visible: boolean = false;
    title: string;
    message: string;
    okText: string;
    cancelText: string;
    negativeOnClick: (e: any) => void;
    positiveOnClick: (e: any) => void;
    subscription: Subscription;

    defaults = {
        title: 'confirmation',
        message: 'Do you want to cancel your changes?',
        cancelText: 'cancel',
        okText: 'comfirm'
    };
    modalElement: any;
    cancelButton: any;
    okButton: any;

    constructor(private modalService: ModalService) {
        modalService.activate = this.activate.bind(this);
    }

    activate(message = this.defaults.message, title = this.defaults.title): Promise<boolean> {
        this.title = title;
        this.message = message;
        this.okText = this.defaults.okText;
        this.cancelText = this.defaults.cancelText;

        let promise = new Promise<boolean>((resolve, reject) => {
            this.negativeOnClick = (e: any) => resolve(false);
            this.positiveOnClick = (e: any) => resolve(true);
            this.show();
        });

        return promise;
    }

    ngOnDestroy() {
        // prevent memory leak when component destroyed
        this.subscription.unsubscribe();
    }

    ngOnInit() {
        this.modalElement = document.getElementById('confirmationModal');
        this.cancelButton = document.getElementById('cancelButton');
        this.okButton = document.getElementById('okButton');
    }

    private show() {

        document.onkeyup = null;

        if (!this.modalElement || !this.cancelButton || !this.okButton) {

            return;
        }

        this.cancelButton.onclick = ((e: any) => {
            e.preventDefault();
            if (this.negativeOnClick(e)  !== null) {
                this.hideDialog();
            }
        });

        this.okButton.onclick = ((e: any) => {
            e.preventDefault();
            if (this.positiveOnClick(e) !== null) {
                this.hideDialog();
            }
        });

        this.modalElement.onclick = () => {
            this.hideDialog();
            return this.negativeOnClick(null);
        };

        document.onkeyup = (e: any) => {
            if (e.which === KEY_ESC) {
                this.hideDialog();
            }
        };

        document.getElementById("openModalButton").click();
    }

    private hideDialog() {
        document.getElementById("cancelButton").click();
    }

}
