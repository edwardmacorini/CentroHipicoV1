import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { combineLatest, map, Observable, startWith } from 'rxjs';
import { Carrera, CarreraDetalle, CarreraEjemplar } from 'src/app/core/models/carrera';
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
  ejemplares: CarreraEjemplar[] = [];
  ejemplaresFiltrados: Observable<CarreraEjemplar[]> | undefined;

  apuestaForm = new FormGroup({
    cliente: new FormControl('', [Validators.required]),
    ejemplar: new FormControl('', [Validators.required]),
    montoApuesta: new FormControl('', [Validators.required]),
  })

  constructor(
    public dialogRef: MatDialogRef<AgregarApuestaDialog>,
    @Inject(MAT_DIALOG_DATA) public data: dialogData,
    private carreraService: CarreraService,
    private clienteService: ClienteService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {
    this.carrera = data.carrera;
  }

  ngOnInit(): void {
    this.cargarDatos();
    this.clientesFiltrados = this.apuestaForm.get('cliente')?.valueChanges.pipe(
      startWith(''),
      map(val => (typeof val === 'string' ? val : val.nombre)),
      map(nombre => nombre ? this.clienteFilter(nombre) : this.clientes.slice())
    );
  }

  openDialog(event: Event): void {
    event.preventDefault();
    const dialogRef = this.dialog.open(AgregarClienteComponent, {
      width: '350px'
    });

    dialogRef.afterClosed().subscribe(result => {
      this.cargarDatos();
    });
  }

  cargarDatos() {
    combineLatest([
      this.clienteService.obtenerClientes(),
      this.carreraService.carrera$
    ])
      .subscribe(([clientes, carrera]) => {
        this.clientes = clientes;
        this.ejemplares = carrera?.ejemplares ?? [];
      });
  }

  private clienteFilter(nombre: string): Cliente[] {
    const filterValue = nombre.trim().toLowerCase();
    return this.clientes.filter(option => option.nombre.trim().toLowerCase().includes(filterValue));
  }

  onSubmit(event: Event) {
    event.preventDefault();
    if (this.apuestaForm.valid) {
      let cliente = this.clientes.find(x => x.nombre == this.apuestaForm.get('cliente')?.value);
      if (cliente) {
        let body: CarreraDetalle = {
          id: 0,
          idCarrera: this.carrera.id,
          idCliente: cliente.id ?? 0,
          idEjemplar: this.apuestaForm.get('ejemplar')?.value,
          montoApuesta: this.apuestaForm.get('montoApuesta')?.value
        };
        //TODO Mostrar mensaje de error
        console.log(body);

        this.carreraService.agregarApuesta(body)
          .subscribe(
            () => {
              this.carreraService.refrescarCarrera(this.carrera.id);
              this.snackBar.open('Apuesta agregada exitosamente..!', 'X', { duration: 2 * 1000 });
              this.onClose();
            }
          );
      }
    }
  }

  onClose(): void {
    this.dialogRef.close();
  }

}