import { Component, Input, Output, OnInit, OnDestroy, EventEmitter, ElementRef, ViewChild, HostBinding } from '@angular/core';
import { NotificationType, NotificationEmit } from '../../services/notification.service';
import { Subscription } from 'rxjs/Subscription';
import { Observable, Scheduler } from 'rxjs/Rx';
import { trigger, state, style, animate, transition, AnimationBuilder, AnimationPlayer } from '@angular/animations';

@Component({
    selector: 'notification',
    templateUrl: './notification.component.html',
    styleUrls: ['./notification.component.css'],
    animations: [
        trigger("notificationState", [
            state('show', style({
                height: '200px'
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

    @ViewChild("notif") notifElement: ElementRef;

    @Input() hostElementHeight: string;
    //set hostElementHeight(height: number) {
    //    this.elementHeight = height;
    //}
    @Input() elementHeight: number;
    @Input() type: NotificationType;

    @Input() text: string;

    @Output()
    onClose = new EventEmitter<any>();

    state: string;

    animationPlayer: AnimationPlayer;

    private _positionY: number;

    get positionY(): number {
        return this._positionY;
    }

    @Input()
    set positionY(newY: number) {
        this._positionY = newY;
        this.moveToY(newY);
    }

    constructor(private animBuilder: AnimationBuilder, private element: ElementRef) {  }

    ngOnInit(): void {
        this.hostElementHeight = this.element.nativeElement.style.height;
        this.state = "show";
        this._observ = Observable.timer(3000);
        // TODO
        //this._observableNotification = this._observ.subscribe(t => { console.log(this.element.nativeElement.style); this.beginClose(); });
        this._observableNotification = this._observ.subscribe(t => this.hostElementHeight = '300px');
    }

    beginClose(): void {
        this.state = 'close';
        this._observ = Observable.timer(500);
        this._observableNotification = this._observ.subscribe(t => this.onClose.emit(null));
    }

    ngOnDestroy(): void {
        this._observableNotification.unsubscribe();
    }

    moveToY(newY: number): void {
        const progressAnimation = this.animBuilder.build([
            animate('500ms', style({
                'bottom': newY
            }))
        ]);
        this.animationPlayer = progressAnimation.create(this.notifElement.nativeElement);
        this.animationPlayer.onDone(() => {
            console.log('ANIMATION DONE');
            // there is no notion of 'trigger' or 'state' here,
            // so the only thing this event gives you is the 'phaseName',
            // which you already know...
            // But the done callback is here and you can do whatever you might need to do
            // for when the animation ends
        });
        this.animationPlayer.play();        
    }

}