import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import * as moment from 'moment';
import 'moment/locale/es'
import { map } from 'rxjs';
import { BaseResponse } from 'src/app/core/models/base-response';
import { Carrera } from 'src/app/core/models/carrera';
import { CarreraService } from 'src/app/core/services/carrera/carrera.service';
import { EliminarCarreraDialogComponent } from './components/eliminar-carrera/eliminar-carrera.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  carreraDisplayedColumns: string[] = ['id', 'fechaCarrera', 'ubicacion', 'estado', 'accion'];
  carreraDataSource: MatTableDataSource<Carrera> = new MatTableDataSource<Carrera>([]);

  now: any;

  constructor(
    private carreraService: CarreraService,
    private router: Router,
    private dialog: MatDialog,
    private snackBar: MatSnackBar) {
    this.now = moment();
  }

  ngOnInit(): void {
    this.cargarDatos();
  }

  cargarDatos() {
    this.carreraService.obtenerCarreras()
      // .pipe(
      //   map((carreras: Carrera[]) => {
      //     let result: any;
      //     carreras.filter(x => x.fechaCarrera >= new Date())
      //   })
      // )
      .subscribe(val => {
        console.log(val);
        this.carreraDataSource = new MatTableDataSource<Carrera>(val);
      });
  }

  verCarrera(idCarrera: number) {
    this.carreraService.obtenerCarrera(idCarrera)
      .subscribe({
        next: (response) => {
          this.carreraService.setCarreraState = response;
          this.router.navigate(['/carrera']);
        },
        error: () => this.snackBar.open('Error en conexión con el servidor.', 'X', { duration: 2 * 1000 })
      });
  }

  eliminarCarrera(carrera: Carrera) {
    const dialogRef = this.dialog.open(EliminarCarreraDialogComponent, {
      width: '350px',
      data: { idCarrera: carrera.id },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.carreraService.eliminarCarrera(carrera)
          .subscribe({
            next: () => this.snackBar.open(`Carrera Nº ${carrera.id} eliminada exitosamente..!`, 'X', { duration: 2 * 1000 }),
            error: (err) => {
              let error: BaseResponse = err.error;
              if (error)
                this.snackBar.open(error.message, 'X', { duration: 2 * 1000 });
              else this.snackBar.open('Error en conexión con el servidor.', 'X', { duration: 2 * 1000 });
            },
            complete: () => this.cargarDatos()
          });
      }
    });
  }

}

/*
// this.carreraDataSource = new MatTableDataSource<Carrera>([
//   {
//     id: 5,
//     fechaCarrera: new Date("2021-12-22T00:00:00"),
//     estaActiva: false,
//     estaCerrada: false,
//     ganador: null,
//     montoGanancia: null,
//     montoSubTotal: null,
//     montoTotal: null,
//     ubicacion: "hipodromo"
//   },
//   {
//     id: 3,
//     fechaCarrera: new Date("2021-11-16T08:00:24"),
//     estaActiva: false,
//     estaCerrada: false,
//     ganador: null,
//     montoGanancia: null,
//     montoSubTotal: null,
//     montoTotal: null,
//     ubicacion: "local"
//   }
// ])
*/