import { NgModule } from "@angular/core";
import { LayoutComponent } from "./components";


@NgModule({
    declarations:[
        LayoutComponent
    ],
    exports:[
        LayoutComponent
    ]
})
export class SharedModule{

}