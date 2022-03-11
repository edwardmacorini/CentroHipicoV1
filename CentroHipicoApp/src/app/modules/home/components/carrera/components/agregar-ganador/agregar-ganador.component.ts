import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { Carrera, CarreraResponse } from 'src/app/core/models/carrera';
import { Cliente } from 'src/app/core/models/cliente';
import { Ejemplar } from 'src/app/core/models/ejemplar';
import { CarreraService } from 'src/app/core/services/carrera/carrera.service';

@Component({
  selector: 'app-agregar-ganador',
  templateUrl: './agregar-ganador.component.html',
  styleUrls: ['./agregar-ganador.component.scss'],
})
export class AgregarGanadorComponent implements OnInit {
  @Input() isDisabled: boolean | undefined;
  @Input() carrera: Carrera | undefined;

  constructor(private dialog: MatDialog) {}

  ngOnInit(): void {}

  openDialog(): void {
    if (!this.isDisabled) {
      const dialogRef = this.dialog.open(AgregarGanadorDialog, {
        width: '295px',
        data: { carrera: this.carrera },
      });

      dialogRef.afterClosed().subscribe((result) => {
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
export class AgregarGanadorDialog implements OnInit {
  carrera: CarreraResponse | null = null;

  ejemplares: Ejemplar[] = [];

  ganadorForm = new FormGroup({
    ejemplar: new FormControl('', [Validators.required]),
  });

  constructor(
    public dialogRef: MatDialogRef<AgregarGanadorDialog>,
    @Inject(MAT_DIALOG_DATA) public data: dialogData,
    private carreraService: CarreraService
  ) {
  }

  ngOnInit(): void {
    this.carreraService.carrera$.subscribe((val) => {
      console.log(this.carrera = val);
      
      val?.ejemplares.forEach((x) => {
        if(x.ejemplar) this.ejemplares.push(x.ejemplar);
      });
    });
  }

  onSubmit() {
    if(this.ganadorForm.valid) {
      console.log(this.ganadorForm.value.ejemplar);
      let aux = this.carrera?.detalles.find(x => x.ejemplar.ejemplar.id == this.ganadorForm.value.ejemplar);
      console.log(aux);
    }
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
