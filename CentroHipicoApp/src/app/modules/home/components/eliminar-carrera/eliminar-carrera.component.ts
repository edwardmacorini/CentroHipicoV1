import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  templateUrl: './eliminar-carrera.component.html',
  styleUrls: ['./eliminar-carrera.component.scss']
})
export class EliminarCarreraDialogComponent implements OnInit {

  idCarrera: number;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<EliminarCarreraDialogComponent>
  ) {
    this.idCarrera = data.idCarrera;
  }

  ngOnInit(): void {
  }

  onClose(value: boolean): void {
    this.dialogRef.close(value);
  }

}
