import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import {RouterModule}  from "@angular/router"; 
import {FormsModule} from "@angular/forms";  
import { AppComponent }  from './app';
//import 'rxjs/Rx';

import { AuthenticationService } from './authentication.service';
import { AuthenticationProxy } from './authentication.proxy';
import { MenuComponent } from "./menu.component";
import { HomeComponent } from "./components/home.component";
import { LoginComponent } from "./components/login.component";
import { NotificationComponent } from "./components/notification.component";
import { NotificationsListComponent } from "./components/notifications-list.component";
import { NotificationService } from "./services/notification.service";

import { AppRouting } from "./app.route";



let directives: any[] = [
    AppComponent,
    MenuComponent,
    HomeComponent,
    LoginComponent,
    NotificationComponent,
    NotificationsListComponent,
];


@NgModule({
    imports: [BrowserModule, HttpModule, FormsModule, RouterModule, AppRouting],
    declarations: [directives],
    bootstrap: [AppComponent],
    providers: [AuthenticationService, AuthenticationProxy, NotificationService]
})
export class AppModule { }