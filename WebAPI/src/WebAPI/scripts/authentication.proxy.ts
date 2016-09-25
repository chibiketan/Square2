import { Injectable } from "@angular/core";
import { AuthenticateRequest } from "./viewmodel/authenticate-request";
import { AuthenticateResponse } from "./viewmodel/authenticate-response";

@Injectable()
export class AuthenticationProxy {
    constructor() {
        console.debug("Construction de AuthenticationProxy");
    }

    authenticate(request: AuthenticateRequest): Promise<AuthenticateResponse> {
        var t = new AuthenticateResponse();

        t.message = "test";
        t.result = false;
        return Promise.resolve(t);
    }
}