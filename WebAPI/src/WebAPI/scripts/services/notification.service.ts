import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class NotificationService {
    private addEvent: Subject<NotificationEmit> = new Subject();

    addEvent$ = this.addEvent.asObservable();

    info(text: string) {
        this.addEvent.next(new NotificationEmit(NotificationType.Info, text));
    }
}

export class NotificationEmit {
    constructor(public type: NotificationType, public text: string) {
        
    }
}

export enum NotificationType {
    Info,
    Error
}