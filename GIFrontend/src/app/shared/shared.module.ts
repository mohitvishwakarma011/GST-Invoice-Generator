import { NgModule } from "@angular/core";
import { LayoutComponent } from "./components";
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';

@NgModule({
    imports:[
        MatFormFieldModule,
        MatButtonModule,
        MatInputModule
    ],
    declarations:[
        LayoutComponent
    ],
    exports:[
        LayoutComponent
    ]
})
export class SharedModule{

}