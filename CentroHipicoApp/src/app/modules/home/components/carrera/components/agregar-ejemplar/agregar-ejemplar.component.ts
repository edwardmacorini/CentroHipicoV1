import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { map, Observable, startWith } from 'rxjs';
import { Carrera, CarreraEjemplar } from 'src/app/core/models/carrera';
import { Cliente } from 'src/app/core/models/cliente';
import { Ejemplar } from 'src/app/core/models/ejemplar';
import { CarreraService } from 'src/app/core/services/carrera/carrera.service';
import { EjemplarService } from 'src/app/core/services/ejemplar/ejemplar.service';
import { AgregarEjemplarComponent as agregarEjemplarComponent } from 'src/app/shared/components/agregar-ejemplar/agregar-ejemplar.component';

@Component({
  selector: 'app-agregar-ejemplar',
  templateUrl: './agregar-ejemplar.component.html',
  styleUrls: ['./agregar-ejemplar.component.scss']
})
export class AgregarEjemplarComponent implements OnInit {

  @Input() isDisabled: boolean | undefined;
  @Input() carrera: Carrera | undefined;

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openDialog(): void {
    if (!this.isDisabled) {
      const dialogRef = this.dialog.open(AgregarEjemplarDialog, {
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
  selector: 'agregar-ejemplar-dialog',
  templateUrl: 'agregar-ejemplar.dialog.html',
})
export class AgregarEjemplarDialog {

  carrera: Carrera;

  ejemplares: Ejemplar[] = [];
  ejemplaresFiltrados: Observable<Ejemplar[]> | undefined;

  ejemplarForm = new FormGroup({
    ejemplar: new FormControl('', [Validators.required]),
    esFavorito: new FormControl(false)
  });

  constructor(
    public dialogRef: MatDialogRef<AgregarEjemplarDialog>,
    @Inject(MAT_DIALOG_DATA) public data: dialogData,
    private carreraService: CarreraService,
    private ejemplarService: EjemplarService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {
    this.carrera = data.carrera;
  }

  ngOnInit(): void {
    this.obtenerEjemplares();
    this.ejemplaresFiltrados = this.ejemplarForm.get('ejemplar')?.valueChanges.pipe(
      startWith(''),
      map(val => (typeof val === 'string' ? val : val.nombre)),
      map(nombre => nombre ? this.filter(nombre) : this.ejemplares.slice())
    );
  }

  openDialog(event: Event): void {
    event.preventDefault();
    const dialogRef = this.dialog.open(agregarEjemplarComponent, {
      width: '350px'
    });

    dialogRef.afterClosed().subscribe(() => {
      this.obtenerEjemplares();
    });
  }

  obtenerEjemplares() {
    this.ejemplarService.obtenerEjemplares()
      .subscribe((res) => this.ejemplares = res);
  }

  private filter(nombre: string): Ejemplar[] {
    const filterValue = nombre.trim().toLowerCase();
    return this.ejemplares.filter(option => option.nombre.trim().toLowerCase().includes(filterValue));
  }

  onSubmit(event: Event) {
    event.preventDefault();
    if (this.ejemplarForm.valid) {
      let ejemplar = this.ejemplares.find(x => x.nombre == this.ejemplarForm.get('ejemplar')?.value);
      if (ejemplar) {
        let body: CarreraEjemplar = {
          id: 0,
          idCarrera: this.carrera.id,
          ejemplar: ejemplar,
          esFavorito: this.ejemplarForm.get('esFavorito')?.value
        }
        this.carreraService.agregarEjemplar(body)
          .subscribe(() => {
            this.carreraService.refrescarCarrera(this.carrera.id);
            this.snackBar.open('Ejemplar agregado exitosamente..!', 'X', { duration: 2 * 1000 });
            this.onClose();
          });
      }
    }
  }

  onClose(): void {
    this.dialogRef.close();
  }

}