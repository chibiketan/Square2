System.register(["@angular/core", "./viewmodel/authenticate-response"], function(exports_1, context_1) {
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
    var core_1, authenticate_response_1;
    var AuthenticationProxy;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (authenticate_response_1_1) {
                authenticate_response_1 = authenticate_response_1_1;
            }],
        execute: function() {
            AuthenticationProxy = (function () {
                function AuthenticationProxy() {
                    console.debug("Construction de AuthenticationProxy");
                }
                AuthenticationProxy.prototype.authenticate = function (request) {
                    var t = new authenticate_response_1.AuthenticateResponse();
                    t.message = "test";
                    t.result = false;
                    return Promise.resolve(t);
                };
                AuthenticationProxy = __decorate([
                    core_1.Injectable(), 
                    __metadata('design:paramtypes', [])
                ], AuthenticationProxy);
                return AuthenticationProxy;
            }());
            exports_1("AuthenticationProxy", AuthenticationProxy);
        }
    }
});
