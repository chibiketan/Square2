System.register(['@angular/core'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1;
    var AppComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            }],
        execute: function() {
            AppComponent = (function () {
                function AppComponent() {
                }
                AppComponent = __decorate([
                    core_1.Component({
                        selector: 'my-app',
                        template: "<div class=\"container\">\n    <div class=\"col-xs-2\"><menu></menu></div>\n    <div class=\"col-xs-10\">\n        <div class=\"container\">\n            <form class=\"form-signin\">\n                <h2 class=\"form-signin-heading\">Please sign in</h2>\n                <label for=\"inputEmail\" class=\"sr-only\">Email address</label>\n                <input type=\"email\" id=\"inputEmail\" class=\"form-control\" placeholder=\"Email address\" required autofocus>\n                <label for=\"inputPassword\" class=\"sr-only\">Password</label>\n                <input type=\"password\" id=\"inputPassword\" class=\"form-control\" placeholder=\"Password\" required>\n                <div class=\"checkbox\">\n                    <label>\n                        <input type=\"checkbox\" value=\"remember-me\"> Remember me\n                    </label>\n                </div>\n                <button class=\"btn btn-lg btn-primary btn-block\" type=\"submit\">Sign in</button>\n            </form>\n        </div>\n    </div>\n</div>"
                    }), 
                    __metadata('design:paramtypes', [])
                ], AppComponent);
                return AppComponent;
            }());
            exports_1("AppComponent", AppComponent);
        }
    }
});
