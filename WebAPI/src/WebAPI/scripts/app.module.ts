import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { AppComponent }  from './app';
import 'rxjs/Rx';

import { AuthenticationService } from './authentication.service';
import { MenuComponent } from "./menu.component";


@NgModule({
    imports: [BrowserModule, HttpModule],
    declarations: [AppComponent, MenuComponent],
    bootstrap: [AppComponent],
    providers: [AuthenticationService]
})
export class AppModule { }