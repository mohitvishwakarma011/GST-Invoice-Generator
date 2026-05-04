import { inject, Injectable } from "@angular/core";
import { Constants } from "./constants";
import { jwtDecode } from 'jwt-decode';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AppDate } from "./app.date";

@Injectable({
    providedIn: 'root'
})
export class AppUtils {

    private readonly _snackbar = inject(MatSnackBar);

    public isUserAuthenticated(): boolean {
        var tokenItem = this._getToken();
        if (this.isNullOrEmpty(tokenItem)) return false;
        else {
            var decodedToken: any = this.getDecodedToken();
            if (AppDate.unixToDate(decodedToken.exp) < AppDate.getCurrentDate())
                return true;
            else return false;
        }
    }

    public isNullOrEmpty(value: string): boolean {
        if (value === null || value === '' || value === undefined) return true;
        else return false;
    }

    public getDecodedToken(): string {
        this.CheckTokenExists();
        const token = this._getToken();
        return jwtDecode(token);
    }

    public CheckTokenExists(): boolean {
        var tokenItem = this._getToken();
        if (this.isNullOrEmpty(tokenItem)) {
            this.ShowSnackbar("Unothorized user");
            return false;
        }
        return true;
    }

    public ShowSnackbar(message: string, action: string = Constants.snackbarDefaultActionString): void {
        this._snackbar.open(message, action);
    }

    private _getToken():string{
        return localStorage.getItem(Constants.tokenKey);
    }
}