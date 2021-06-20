import { Injectable, Optional, SkipSelf } from '@angular/core';
import { Subject } from 'rxjs/Subject';

export interface ToastMessage {
    message: string;
    cssClass: string;
}

@Injectable()
export class ToastService {
    public toastSubject = new Subject<ToastMessage>();

    public toastState = this.toastSubject.asObservable();

    constructor( @Optional() @SkipSelf() prior: ToastService) {
        if (prior) {
            return prior;
        } else {
        }
    }

    activate(message?: string, cssClass?: string) {
        this.toastSubject.next(<ToastMessage>{ message: message, cssClass: cssClass });
    }
}

