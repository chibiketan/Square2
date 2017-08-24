"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var http_1 = require("@angular/http");
var router_1 = require("@angular/router");
var forms_1 = require("@angular/forms");
var app_1 = require("./app");
require("rxjs/Rx");
var authentication_service_1 = require("./authentication.service");
var authentication_proxy_1 = require("./authentication.proxy");
var menu_component_1 = require("./menu.component");
var home_component_1 = require("./components/home.component");
var login_component_1 = require("./components/login.component");
var app_route_1 = require("./app.route");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [platform_browser_1.BrowserModule, http_1.HttpModule, forms_1.FormsModule, router_1.RouterModule, app_route_1.AppRouting],
        declarations: [app_1.AppComponent, menu_component_1.MenuComponent, home_component_1.HomeComponent, login_component_1.LoginComponent],
        bootstrap: [app_1.AppComponent],
        providers: [authentication_service_1.AuthenticationService, authentication_proxy_1.AuthenticationProxy]
    })
], AppModule);
exports.AppModule = AppModule;
