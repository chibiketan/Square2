import { Component, Input, Output, OnInit, OnDestroy, EventEmitter } from '@angular/core';
import { NotificationType, NotificationEmit } from '../../services/notification.service';
import { Subscription } from 'rxjs/Subscription';
import { Observable, Scheduler } from 'rxjs/Rx';
import { trigger, state, style, animate, transition } from '@angular/animations';

@
Component({
    selector: 'notification',
    templateUrl: './notification.component.html',
    styleUrls: ['./notification.component.css'],
    animations: [
        trigger("notificationState", [
            state('show', style({
                height: '*'
            })),
            state('close', style({
                 height: 0   
            })),
            transition(":enter", [
                style({height: 0}),
                animate("0.5s")]),
            transition("show => close", [
                //style({ height: '*' }),
                animate("0.5s")
            ])
        ])
    ]
})
export class NotificationComponent implements OnInit, OnDestroy {
    private _observableNotification: Subscription;
    private _observ: Observable<number>;

    @Input() type: NotificationType;

    @Input() text: string;

    @Output()
    onClose = new EventEmitter<any>();

    state: string;

    notifications: NotificationEmit[];

    onNewNotification(notification: NotificationEmit): void {
        this.notifications.push(notification);
    }


    ngOnInit(): void {
        this.state = "show";
        this._observ = Observable.timer(3000);
        // TODO
        this._observableNotification = this._observ.subscribe(t => this.beginClose());
    }

    beginClose(): void {
        this.state = 'close';
        this._observ = Observable.timer(500);
        this._observableNotification = this._observ.subscribe(t => this.onClose.emit(null));
    }

    ngOnDestroy(): void {
        this._observableNotification.unsubscribe();
    }

}