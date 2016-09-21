import { Component } from "@angular/core"
import { AuthenticationService } from "./authentication.service"

@Component({
    selector: "menu",
    templateUrl: "views/menu.html"
})
export class MenuComponent {
    constructor(private authenticationService: AuthenticationService) {
        console.debug("Construction del'objet MenuComponent");
    }
}