import { Component, OnInit, OnDestroy } from '@angular/core';
import { NotificationService, NotificationType, NotificationEmit } from '../../services/notification.service';
import { Subscription } from 'rxjs/Subscription';
import { Observable, Scheduler } from 'rxjs/Rx';

@Component({
    selector: 'notifications-list',
    templateUrl: './notifications-list.component.html',
    styleUrls: ['./notifications-list.component.css']
})
export class NotificationsListComponent implements OnInit, OnDestroy {
    private _addEventSubscription: Subscription;

    notifications: NotificationItem[] = [];

    constructor(private _notificationService: NotificationService) {
    }

    onNewNotification(notification: NotificationEmit): void {
        let el = new NotificationItem(notification.type, notification.text, this.i++);

        el.observ = Observable.timer(3000);
        el.observableNotification = el.observ.subscribe(t => this.onNotificationClose(el));
        this.notifications.push(el);
    }

    onNotificationClose(notification: NotificationItem): void {
        let index = this.notifications.indexOf(notification);

        notification.observableNotification.unsubscribe();
        this.notifications.splice(index, 1);
    }

    ngOnInit(): void {
        this._addEventSubscription = this._notificationService.addEvent$.subscribe(
            notification => {
                this.onNewNotification(notification);
            }
        );
    }

    ngOnDestroy(): void {
        this._addEventSubscription.unsubscribe();
    }

    i: number = 0;
}

class NotificationItem {
    observableNotification: Subscription;
    observ: Observable<number>;

    constructor(public type: NotificationType, public text: string, public i: number) {  }
}