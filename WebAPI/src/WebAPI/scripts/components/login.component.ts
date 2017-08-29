import { Component } from "@angular/core"
import { NotificationService } from '../services/notification.service';

@Component({
    templateUrl: "views/login.html"
})
export class LoginComponent {

    constructor(private _notificationService: NotificationService) {  }
    onClick(): void {
        this._notificationService.info("Demande de connexion");
    }
}