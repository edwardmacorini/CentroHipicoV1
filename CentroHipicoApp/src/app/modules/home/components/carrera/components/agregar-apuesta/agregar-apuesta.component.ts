import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { combineLatest, Observable } from 'rxjs';
import { Carrera, CarreraDetalle } from 'src/app/core/models/carrera';
import { Cliente } from 'src/app/core/models/cliente';
import { Ejemplar } from 'src/app/core/models/ejemplar';
import { CarreraService } from 'src/app/core/services/carrera/carrera.service';
import { ClienteService } from 'src/app/core/services/cliente/cliente.service';
import { AgregarClienteComponent } from 'src/app/shared/components/agregar-cliente/agregar-cliente.component';

@Component({
  selector: 'app-agregar-apuesta',
  templateUrl: './agregar-apuesta.component.html',
  styleUrls: ['./agregar-apuesta.component.scss']
})
export class AgregarApuestaComponent implements OnInit {

  @Input() isDisabled: boolean | undefined;
  @Input() carrera: Carrera | undefined;

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openDialog(): void {
    if (!this.isDisabled) {
      const dialogRef = this.dialog.open(AgregarApuestaDialog, {
        width: '350px',
        data: { carrera: this.carrera },
      });

      dialogRef.afterClosed().subscribe(result => {
        console.log('The dialog was closed');
      });
    }
  }
}

interface dialogData {
  carrera: Carrera;
}

@Component({
  selector: 'agregar-apuesta-dialog',
  templateUrl: 'agregar-apuesta.dialog.html',
})
export class AgregarApuestaDialog {

  carrera: Carrera;

  clientes: Cliente[] = [];
  clientesFiltrados: Observable<Cliente[]> | undefined;
  ejemplares: Ejemplar[] = [];
  ejemplaresFiltrados: Observable<Ejemplar[]> | undefined;

  apuestaForm = new FormGroup({
    cliente: new FormControl('', [Validators.required]),
    ejemplar: new FormControl('', [Validators.required]),
    montoApuesta: new FormControl('', [Validators.required]),
  })

  constructor(
    public dialogRef: MatDialogRef<AgregarApuestaDialog>,
    @Inject(MAT_DIALOG_DATA) public data: dialogData,
    private dialog: MatDialog,
    private carreraService: CarreraService,
    private clienteService: ClienteService
  ) {
    this.carrera = data.carrera;
  }

  openDialog(event: Event): void {
    event.preventDefault();
    const dialogRef = this.dialog.open(AgregarClienteComponent, {
      width: '350px'
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  obtenerDatos() {
    combineLatest([
      this.clienteService.obtenerClientes(),
      this.carreraService.
    ])
    this.clienteService.obtenerClientes()
      .subscribe(result => this.clientes = result);
  }

  onSubmit(event: Event) {
    event.preventDefault();
    let body: CarreraDetalle = {
      id: 0,
      idCarrera: this.carrera.id,
      cliente:
        ejemplar
      montoApuesta
    }
    this.carreraService.agregarApuesta()

  }

  onClose(): void {
    this.dialogRef.close();
  }

}