import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { AgregarCarreraComponent, AgregarCarreraDialog } from './components/agregar-carrera/agregar-carrera.component';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { CarreraComponent } from './components/carrera/carrera.component';
import { AbrirCerrarCarreraComponent, AbrirCerrarCarreraDialog } from './components/carrera/components/abrir-cerrar-carrera/abrir-cerrar-carrera.component';
import { AgregarApuestaComponent, AgregarApuestaDialog } from './components/carrera/components/agregar-apuesta/agregar-apuesta.component';
import { AgregarEjemplarComponent, AgregarEjemplarDialog } from './components/carrera/components/agregar-ejemplar/agregar-ejemplar.component';
import { AgregarGanadorComponent, AgregarGanadorDialog } from './components/carrera/components/agregar-ganador/agregar-ganador.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatChipsModule } from '@angular/material/chips';
import { MatNativeDateModule } from '@angular/material/core';
import {
  NgxMatDatetimePickerModule,
  NgxMatNativeDateModule,
  NgxMatTimepickerModule
} from '@angular-material-components/datetime-picker';
import { EliminarCarreraDialogComponent } from './components/eliminar-carrera/eliminar-carrera.component';
import { ComprobanteComponent, ComprobanteDialog } from './components/carrera/components/comprobante/comprobante.component';

@NgModule({
  declarations: [
    HomeComponent,
    AgregarCarreraComponent,
    AgregarCarreraDialog,
    CarreraComponent,
    AbrirCerrarCarreraComponent,
    AbrirCerrarCarreraDialog,
    AgregarApuestaComponent,
    AgregarApuestaDialog,
    AgregarEjemplarComponent,
    AgregarEjemplarDialog,
    AgregarGanadorComponent,
    AgregarGanadorDialog,
    EliminarCarreraDialogComponent,
    ComprobanteComponent,
    ComprobanteDialog
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HomeRoutingModule,
    SharedModule,
    MatChipsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    NgxMatDatetimePickerModule,
    NgxMatNativeDateModule,
    NgxMatTimepickerModule,
  ],
  exports: [
    HomeComponent
  ]
})
export class HomeModule { }
