import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { map } from 'rxjs';
import { Carrera, CarreraDetalle, carreraResponse } from 'src/app/core/models/carrera';
import { CarreraService } from 'src/app/core/services/carrera/carrera.service';

@Component({
  selector: 'app-carrera',
  templateUrl: './carrera.component.html',
  styleUrls: ['./carrera.component.scss']
})
export class CarreraComponent implements OnInit {
  detallesDisplayedColumns: string[] = [/* 'id',  */'idEjemplar', 'idCliente', 'montoApuesta'/* , 'accion' */];
  detallesDataSource: MatTableDataSource<CarreraDetalle> = new MatTableDataSource<CarreraDetalle>([]);

  carrera: Carrera | undefined;

  carrera$ = this.carreraService.carrera$
    .pipe(
      map((val: carreraResponse) => {
        if (val && val.carrera && val.detalles && val.ejemplares) {
          console.log(val);

          this.carrera = val.carrera;
          this.detallesDataSource = new MatTableDataSource<CarreraDetalle>(val.detalles);
        }
        return val;
      })
    );

  constructor(private carreraService: CarreraService) {
  }

  ngOnInit(): void {
  }

  agregarApuesta() {

  }

  agregarEjemplar() {

  }

  agregarGanador() {

  }

}
