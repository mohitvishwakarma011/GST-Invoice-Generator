import { Component, inject, ViewEncapsulation } from "@angular/core";
import { AppUtils } from "src/app/helpers/app.utils";

@Component({
    selector: 'ngx-layout',
    templateUrl: './layout.component.html',
    standalone: false,
    encapsulation: ViewEncapsulation.None
})
export class LayoutComponent {
    protected isAuthenticated: boolean = false;
    private readonly _appUtils: AppUtils = inject(AppUtils);

    constructor() {
        this.isAuthenticated = this._appUtils.isUserAuthenticated();
    }
}