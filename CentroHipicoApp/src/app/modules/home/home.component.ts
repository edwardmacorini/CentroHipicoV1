import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import * as moment from 'moment';
import 'moment/locale/es'
import { Carrera } from 'src/app/core/models/carrera';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  carreraDisplayedColumns: string[] = ['id', 'fechaCarrera', 'ubicacion', 'estado', 'accion'];
  carreraDataSource: MatTableDataSource<Carrera> = new MatTableDataSource<Carrera>([]);

  now: any;

  constructor() {
    this.now = moment();
  }

  ngOnInit(): void {
    this.carreraDataSource = new MatTableDataSource<Carrera>([
      {
        id: 5,
        fechaCarrera: new Date("2021-12-22T00:00:00"),
        estaActiva: false,
        estaCerrada: false,
        ganador: null,
        montoGanancia: null,
        montoSubTotal: null,
        montoTotal: null,
        ubicacion: "hipodromo"
      },
      {
        id: 3,
        fechaCarrera: new Date ("2021-11-16T08:00:24"),
        estaActiva: false,
        estaCerrada: false,
        ganador: null,
        montoGanancia: null,
        montoSubTotal: null,
        montoTotal: null,
        ubicacion: "local"
      }
    ])
  }

}
