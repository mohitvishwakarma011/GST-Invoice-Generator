import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ResourceNotFoundComponent } from './shared/components/resource-not-found/resource-not-found.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  {
    path:'auth',
    children:[
      {
        path:'login',
        component:LoginComponent
      }
    ]
  },
  { path:'**',component:ResourceNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
