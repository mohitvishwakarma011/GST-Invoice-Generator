import { NgModule } from "@angular/core";
import { LayoutComponent } from "./components";
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { ResourceNotFoundComponent } from './components/resource-not-found/resource-not-found.component';
import { BrowserModule } from "@angular/platform-browser";

@NgModule({
    imports: [
    MatFormFieldModule,
    MatButtonModule,
    MatInputModule,
    BrowserModule
],
    declarations:[
        LayoutComponent,
        ResourceNotFoundComponent
    ],
    exports:[
        LayoutComponent,
        ResourceNotFoundComponent
    ]
})
export class SharedModule{

}