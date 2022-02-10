import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SharedModule } from 'src/app/shared/shared.module';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { AgregarCarreraComponent, AgregarCarreraDialog } from './components/agregar-carrera/agregar-carrera.component';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CarreraComponent } from './components/carrera/carrera.component';
import { HomeRoutingModule } from './test-routing.module';
import {MatButtonToggleModule} from '@angular/material/button-toggle';

@NgModule({
  declarations: [
    HomeComponent,
    AgregarCarreraComponent,
    AgregarCarreraDialog,
    CarreraComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MatCardModule,
    MatInputModule,
    MatDividerModule,
    MatButtonModule,
    SharedModule,
    MatIconModule,
    MatTooltipModule,
    MatTableModule,
    MatDialogModule,
    NgxMaterialTimepickerModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonToggleModule
  ],
  exports: [
    HomeComponent
  ]
})
export class HomeModule { }
