import { Component, OnInit, OnDestroy } from '@angular/core';
import { NotificationService, NotificationType, NotificationEmit } from '../../services/notification.service';
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: 'notifications-list',
    templateUrl: './notifications-list.component.html'
})
export class NotificationsListComponent implements OnInit, OnDestroy {
    private _addEventSubscription: Subscription;

    notifications: NotificationEmit[] = [];

    constructor(private _notificationService: NotificationService) {
    }

    onNewNotification(notification: NotificationEmit): void {
        this.notifications.push(notification);
    }

    onNotificationClose(notification: NotificationEmit): void {
        let index = this.notifications.indexOf(notification);

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
}