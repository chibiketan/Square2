System.register(["@angular/core", "rxjs/Subject"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __moduleName = context_1 && context_1.id;
    var core_1, Subject_1, NotificationService, NotificationEmit, NotificationType;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (Subject_1_1) {
                Subject_1 = Subject_1_1;
            }
        ],
        execute: function () {
            NotificationService = (function () {
                function NotificationService() {
                    this.addEvent = new Subject_1.Subject();
                    this.addEvent$ = this.addEvent.asObservable();
                }
                NotificationService.prototype.info = function (text) {
                    this.addEvent.next(new NotificationEmit(NotificationType.Info, text));
                };
                return NotificationService;
            }());
            NotificationService = __decorate([
                core_1.Injectable()
            ], NotificationService);
            exports_1("NotificationService", NotificationService);
            NotificationEmit = (function () {
                function NotificationEmit(type, text) {
                    this.type = type;
                    this.text = text;
                }
                return NotificationEmit;
            }());
            exports_1("NotificationEmit", NotificationEmit);
            (function (NotificationType) {
                NotificationType[NotificationType["Info"] = 0] = "Info";
                NotificationType[NotificationType["Error"] = 1] = "Error";
            })(NotificationType || (NotificationType = {}));
            exports_1("NotificationType", NotificationType);
        }
    };
});
