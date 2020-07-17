import {EventEmitter, Injectable} from "@angular/core";

@Injectable()
export class GlobalEventsManager {
    
    public isAuthorized: EventEmitter<boolean> = new EventEmitter<boolean>();
    public isAdmin: EventEmitter<boolean> = new EventEmitter<boolean>();
    public displayName: EventEmitter<string> = new EventEmitter<string>();

    constructor() {

    }
}