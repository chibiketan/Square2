System.register(["@angular/core", "@angular/platform-browser", "@angular/http", "@angular/router", "@angular/forms", "./app", "rxjs/Rx", "./authentication.service", "./authentication.proxy", "./menu.component", "./components/home.component", "./components/login.component", "./app.route"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __moduleName = context_1 && context_1.id;
    var core_1, platform_browser_1, http_1, router_1, forms_1, app_1, authentication_service_1, authentication_proxy_1, menu_component_1, home_component_1, login_component_1, app_route_1, AppModule;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (platform_browser_1_1) {
                platform_browser_1 = platform_browser_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (forms_1_1) {
                forms_1 = forms_1_1;
            },
            function (app_1_1) {
                app_1 = app_1_1;
            },
            function (_1) {
            },
            function (authentication_service_1_1) {
                authentication_service_1 = authentication_service_1_1;
            },
            function (authentication_proxy_1_1) {
                authentication_proxy_1 = authentication_proxy_1_1;
            },
            function (menu_component_1_1) {
                menu_component_1 = menu_component_1_1;
            },
            function (home_component_1_1) {
                home_component_1 = home_component_1_1;
            },
            function (login_component_1_1) {
                login_component_1 = login_component_1_1;
            },
            function (app_route_1_1) {
                app_route_1 = app_route_1_1;
            }
        ],
        execute: function () {
            AppModule = (function () {
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
            exports_1("AppModule", AppModule);
        }
    };
});
//# sourceMappingURL=app.module.js.map