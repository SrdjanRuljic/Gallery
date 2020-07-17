import { Component, OnInit } from '@angular/core';
import { ToastService } from './toast.service';
import { Subscription } from 'rxjs/Subscription';
import { ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class ToastComponent implements OnInit {

    public defaults = {
        title: '',
        message: '',
        cssClass: 'alert-successToast'
    };
    public toastElement: any;
    public toastSubscription: Subscription;
    public isShow: boolean = false;

    public title: string;
    public message: string;
    public cssClass: string;

    constructor(private toastService: ToastService) {
        this.toastSubscription = this.toastService.toastState.subscribe((toastMessage) => {
            this.activate(toastMessage.message, "", toastMessage.cssClass);
        });
    }

    activate(message = this.defaults.message, title = this.defaults.title, cssClass = this.defaults.cssClass) {
        this.title = title;
        this.message = message;
        this.cssClass = cssClass;

        this.show();
    }

    ngOnInit() {
        this.isShow = false;
        this.toastElement = document.getElementById('toast');
    }

    ngOnDestroy() {
        this.toastSubscription.unsubscribe();
    }

    private show() {
        if (!this.isShow) {
            this.isShow = true;
            this.toastElement.style.opacity = 1;
            this.toastElement.style.zIndex = 9999;
            window.setTimeout(() => this.hide(), 6000);
        }
    }

    private hide() {
        this.isShow = false;
        this.toastElement.style.opacity = 0;
        this.toastElement.style.zIndex = -9999;
    }


}
