import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { map } from 'rxjs';
import { Carrera, CarreraDetalle, CarreraDetalleResponse, CarreraResponse } from 'src/app/core/models/carrera';
import { CarreraService } from 'src/app/core/services/carrera/carrera.service';

@Component({
  selector: 'app-carrera',
  templateUrl: './carrera.component.html',
  styleUrls: ['./carrera.component.scss']
})
export class CarreraComponent implements OnInit {
  detallesDisplayedColumns: string[] = [/* 'id',  */'idEjemplar', 'idCliente', 'montoApuesta'/* , 'accion' */];
  detallesDataSource: MatTableDataSource<CarreraDetalleResponse> = new MatTableDataSource<CarreraDetalleResponse>([]);

  carrera: Carrera | undefined;

  msgForCards: { key: string, value: number, color: string }[] = [];

  carrera$ = this.carreraService.carrera$
    .pipe(
      map((val: CarreraResponse | null) => {
        if (val && val.carrera && val.detalles && val.ejemplares) {
          this.carrera = val.carrera;
          this.msgForCards[0].value = val.carrera.montoGanancia ?? 0;
          this.msgForCards[1].value = val.carrera.montoSubTotal ?? 0;
          this.msgForCards[2].value = val.carrera.montoTotal ?? 0;
          this.detallesDataSource = new MatTableDataSource<CarreraDetalleResponse>(val.detalles);
        }
        return val;
      })
    );

  constructor(private carreraService: CarreraService) {
  }

  ngOnInit(): void {
    this.msgForCards = [
      { key: 'Ganancias', value: 0, color: '#304ffe' },
      { key: 'Subtotal', value: 0, color: '#2962ff' },
      { key: 'Monto Total', value: 0, color: '#00b8d4' },
    ];
  }

}
