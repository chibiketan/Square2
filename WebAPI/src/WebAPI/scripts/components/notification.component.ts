import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { NotificationService, NotificationType, NotificationEmit } from '../services/notification.service';
import { Subscription } from 'rxjs/Subscription';
import { Observable, Scheduler } from 'rxjs/Rx';

@Component({
    selector: 'notification',
    templateUrl: 'views/notification.html'
})
export class NotificationComponent implements OnInit, OnDestroy {
    private _observableNotification: Subscription;

    @Input() type: NotificationType;

    @Input() text: string;

    @Input()
    onClose: ()=>void;

    notifications: NotificationEmit[];

    onNewNotification(notification: NotificationEmit): void {
        this.notifications.push(notification);
    }


    ngOnInit(): void {
        let observ = Observable.timer(10000, null, Scheduler.animationFrame);
        // TODO
        this._observableNotification = observ.subscribe(t => this.onClose());
    }

    ngOnDestroy(): void {
        this._observableNotification.unsubscribe();
    }

}