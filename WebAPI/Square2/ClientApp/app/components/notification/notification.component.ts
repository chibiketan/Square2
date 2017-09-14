import { Component, Input, Output, OnInit, OnDestroy, EventEmitter } from '@angular/core';
import { NotificationType, NotificationEmit } from '../../services/notification.service';
import { Subscription } from 'rxjs/Subscription';
import { Observable, Scheduler } from 'rxjs/Rx';

@Component({
    selector: 'notification',
    templateUrl: './notification.component.html'
})
export class NotificationComponent implements OnInit, OnDestroy {
    private _observableNotification: Subscription;
    private _observ: Observable<number>;

    @Input() type: NotificationType;

    @Input() text: string;

    @Output()
    onClose = new EventEmitter<any>();

    notifications: NotificationEmit[];

    onNewNotification(notification: NotificationEmit): void {
        this.notifications.push(notification);
    }


    ngOnInit(): void {
        this._observ = Observable.timer(3000);
        // TODO
        this._observableNotification = this._observ.subscribe(t => this.onClose.emit(null));
    }

    ngOnDestroy(): void {
        this._observableNotification.unsubscribe();
    }

}