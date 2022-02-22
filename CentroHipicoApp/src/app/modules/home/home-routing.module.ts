import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CarreraComponent } from './components/carrera/carrera.component';
import { HomeComponent } from './home.component';


const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'carrera', component: CarreraComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class HomeRoutingModule { }