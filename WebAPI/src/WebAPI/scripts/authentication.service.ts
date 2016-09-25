import { Injectable } from "@angular/core"
import { AuthenticationProxy } from "./authentication.proxy"

@Injectable()
export class AuthenticationService {
    constructor(private autenticationProxy: AuthenticationProxy) {
        console.debug("Construction d'un objet AuthenticationService");
    }
    
}