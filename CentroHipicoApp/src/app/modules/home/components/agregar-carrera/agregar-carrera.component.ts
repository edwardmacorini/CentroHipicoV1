import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Carrera } from 'src/app/core/models/carrera';
import { CarreraService } from 'src/app/core/services/carrera/carrera.service';

@Component({
  selector: 'app-agregar-carrera',
  templateUrl: './agregar-carrera.component.html',
  styleUrls: ['./agregar-carrera.component.scss']
})
export class AgregarCarreraComponent implements OnInit {

  constructor(public dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(AgregarCarreraDialog, {
      width: '350px',
      // data: { },
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log(result);
    });
  }

}

@Component({
  selector: 'agregar-carrera-dialog',
  templateUrl: 'agregar-carrera.dialog.html',
})
export class AgregarCarreraDialog {

  carreraForm: FormGroup = new FormGroup({
    fechaCarrera: new FormControl('', [Validators.required]),
    ubicacion: new FormControl('', [Validators.required])
  });

  hora: any;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<AgregarCarreraDialog>,
    private carreraService: CarreraService,
    private snackBar: MatSnackBar
  ) { }

  agregarCarrera(event: Event) {
    event.preventDefault();
    if (this.carreraForm.valid) {
      let carrera: Carrera = {
        id: 0,
        fechaCarrera: this.carreraForm.get('fechaCarrera')?.value ?? new Date(),
        estaActiva: false,
        estaCerrada: false,
        ganador: null,
        montoGanancia: null,
        montoSubTotal: null,
        montoTotal: null,
        ubicacion: this.carreraForm.get('ubicacion')?.value ?? ''
      };
      this.carreraService.agregarCarrera(carrera).subscribe(val => {
        this.snackBar.open('Carrera agregada exitosamente..!', 'X', { duration: 2 * 1000 });
        this.onClose();
      })
    }
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
