import { NgModule } from "@angular/core";
import { LayoutComponent } from "./components";
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { ResourceNotFoundComponent } from './components/resource-not-found/resource-not-found.component';

@NgModule({
    imports:[
        MatFormFieldModule,
        MatButtonModule,
        MatInputModule
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