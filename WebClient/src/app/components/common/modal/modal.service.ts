import { Injectable, Optional, SkipSelf } from '@angular/core';

@Injectable()
export class ModalService {
    constructor( @Optional() @SkipSelf() prior: ModalService) {
        if (prior) {
            return prior;
        } else {
        }
    }

    activate: (message?: string, title?: string) => Promise<boolean>;
}