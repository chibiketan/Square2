"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var home_component_1 = require("./components/home.component");
var login_component_1 = require("./components/login.component");
var routes = [
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    },
    {
        path: 'home',
        component: home_component_1.HomeComponent
    },
    {
        path: 'login',
        component: login_component_1.LoginComponent
    }
];
exports.AppRoutingProviders = [];
exports.AppRouting = router_1.RouterModule.forRoot(routes);
//# sourceMappingURL=app.route.js.map