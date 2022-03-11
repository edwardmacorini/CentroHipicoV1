import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Carrera } from 'src/app/core/models/carrera';
import { CarreraService } from 'src/app/core/services/carrera/carrera.service';

@Component({
  selector: 'app-abrir-cerrar-carrera',
  templateUrl: './abrir-cerrar-carrera.component.html',
  styleUrls: ['./abrir-cerrar-carrera.component.scss']
})
export class AbrirCerrarCarreraComponent implements OnInit {

  @Input() opts: boolean | undefined;
  @Input() isDisabled: boolean | undefined;
  @Input() carrera: Carrera | undefined;

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openDialog(): void {
    if (!this.isDisabled) {
      const dialogRef = this.dialog.open(AbrirCerrarCarreraDialog, {
        width: '295px',
        data: { opts: this.opts, carrera: this.carrera },
      });

      dialogRef.afterClosed().subscribe(result => {
        console.log('The dialog was closed');
      });
    }
  }
}

interface dialogData {
  opts: boolean;
  carrera: Carrera;
}

@Component({
  selector: 'abrir-cerrar-carrera-dialog',
  templateUrl: 'abrir-cerrar-carrera.dialog.html',
})
export class AbrirCerrarCarreraDialog {

  opts: boolean;
  carrera: Carrera;

  constructor(
    public dialogRef: MatDialogRef<AbrirCerrarCarreraDialog>,
    @Inject(MAT_DIALOG_DATA) public data: dialogData,
    private carreraService: CarreraService,
    private snackBar: MatSnackBar
  ) {
    this.opts = data.opts;
    this.carrera = data.carrera;
  }

  abrirCarrera() {
    this.carreraService.abrirCarrera(this.carrera.id)
      .subscribe(val => {
        this.carreraService.refrescarCarrera(this.carrera.id);
        this.snackBar.open('Carrera abierta con exito', 'X', { duration: 2 * 1000 });
        this.onClose();
      })
  }

  cerrarCarrera() {
    this.carreraService.cerrarCarrera(this.carrera.id)
      .subscribe(val => {
        this.carreraService.refrescarCarrera(this.carrera.id);
        this.snackBar.open('Carrera cerrada con exito', 'X', { duration: 2 * 1000 });
        this.onClose();
      })
  }

  onClose(): void {
    this.dialogRef.close();
  }

}