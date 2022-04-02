import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from './core/guards/authentication.guard';
import { LoginComponent } from './modules/auth/components/login/login.component';
import { RegisterComponent } from './modules/auth/components/register/register.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent, canActivate: [AuthenticationGuard] },
  { path: 'home', loadChildren: () => import('./modules/home/home.module').then(x => x.HomeModule), canActivate: [AuthenticationGuard] },
  { path: 'reports', loadChildren: () => import('./modules/reports/reports.module').then(x => x.ReportsModule), canActivate: [AuthenticationGuard] },
  { path: 'settings', loadChildren: () => import('./modules/settings/settings.module').then(x => x.SettingsModule), canActivate: [AuthenticationGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
