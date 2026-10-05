import { NgModule } from "@angular/core";
import { LayoutComponent } from "./components";
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ResourceNotFoundComponent } from './components/resource-not-found/resource-not-found.component';
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";

@NgModule({
    imports: [
        MatFormFieldModule,
        MatButtonModule,
        MatInputModule,
        BrowserModule,
        RouterModule,
        CommonModule
    ],
    declarations: [
        LayoutComponent,
        ResourceNotFoundComponent
    ],
    exports: [
        LayoutComponent,
        ResourceNotFoundComponent,
        MatFormFieldModule,
        MatButtonModule,
        MatInputModule,
        BrowserModule,
        RouterModule,
        CommonModule
    ]
})
export class SharedModule {

}