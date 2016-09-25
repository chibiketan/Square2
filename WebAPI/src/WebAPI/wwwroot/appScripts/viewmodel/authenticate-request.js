System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var AuthenticateRequest;
    return {
        setters:[],
        execute: function() {
            AuthenticateRequest = (function () {
                function AuthenticateRequest(login, password) {
                    this.login = login;
                    this.password = password;
                }
                return AuthenticateRequest;
            }());
            exports_1("AuthenticateRequest", AuthenticateRequest);
        }
    }
});
//# sourceMappingURL=authenticate-request.js.map