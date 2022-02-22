import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Carrera } from 'src/app/core/models/carrera';

@Component({
  selector: 'app-agregar-ganador',
  templateUrl: './agregar-ganador.component.html',
  styleUrls: ['./agregar-ganador.component.scss']
})
export class AgregarGanadorComponent implements OnInit {

  @Input() isDisabled: boolean | undefined;
  @Input() carrera: Carrera | undefined;

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openDialog(): void {
    if (!this.isDisabled) {
      const dialogRef = this.dialog.open(AgregarGanadorDialog, {
        width: '295px',
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
  selector: 'agregar-ganador-dialog',
  templateUrl: 'agregar-ganador.dialog.html',
})
export class AgregarGanadorDialog {

  carrera: Carrera;

  constructor(
    public dialogRef: MatDialogRef<AgregarGanadorDialog>,
    @Inject(MAT_DIALOG_DATA) public data: dialogData,
  ) {
    this.carrera = data.carrera;
  }

  onClose(): void {
    this.dialogRef.close();
  }

}