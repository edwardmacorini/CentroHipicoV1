import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import {
  Carrera,
  CarreraGanador,
  CarreraResponse,
} from 'src/app/core/models/carrera';
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
      this.dialog.open(AgregarGanadorDialog, {
        width: '295px',
        data: { carrera: this.carrera },
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
    private carreraService: CarreraService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.carreraService.carrera$.subscribe((val) => {
      this.carrera = val;
      val?.ejemplares.forEach((x) => {
        if (x.ejemplar) this.ejemplares.push(x.ejemplar);
      });
    });
  }

  onSubmit() {
    if (this.ganadorForm.valid) {
      let idCliente = this.carrera?.detalles.find(
        (x) => x.ejemplar.ejemplar.id == this.ganadorForm.value.ejemplar
      )?.cliente?.id;
      let ganador: CarreraGanador = {
        idCarrera: this.carrera?.carrera.id ?? 0,
        idCliente: idCliente ?? 0,
      };
      this.carreraService.agregarGanador(ganador).subscribe(() => {
        this.carreraService.refrescarCarrera(this.carrera?.carrera.id ?? 0);
        this.snackBar.open('Ganador agregado exitosamente..!', 'X', {
          duration: 2 * 1000,
        });
        this.onClose();
      });
    }
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
