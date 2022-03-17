import { Component, Inject, Input, OnInit } from '@angular/core';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { Carrera } from 'src/app/core/models/carrera';
import { Ticket } from 'src/app/core/models/ticket';
import { CarreraService } from 'src/app/core/services/carrera/carrera.service';
import { PrinterService } from 'src/app/core/services/printer/printer.service';

@Component({
  selector: 'app-comprobante',
  templateUrl: './comprobante.component.html',
  styleUrls: ['./comprobante.component.scss'],
})
export class ComprobanteComponent implements OnInit {
  @Input() isDisabled: boolean | undefined;
  @Input() carrera: Carrera | undefined;

  constructor(private dialog: MatDialog) {}

  ngOnInit() {}

  openDialog(): void {
    if (!this.isDisabled) {
      this.dialog.open(ComprobanteDialog, {
        width: '385px',
        data: { carrera: this.carrera },
      });
    }
  }
}

@Component({
  selector: 'agregar-ganador-dialog',
  templateUrl: 'comprobante.dialog.html',
})
export class ComprobanteDialog implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<ComprobanteDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private carreraService: CarreraService,
    private printerService: PrinterService
  ) {}

  ngOnInit(): void {
    console.log(this.data.carrera);
  }

  onSubmit() {
    let ticket: Ticket[] = [
      {
        razonSocial: 'edward ca',
        rif: '26529492-4',
        fechaDocumento: new Date().toJSON(),
        numeroCarrera: 55,
        nombreCliente: 'edward 3macorini',
        detalles: [{ numeroEjemplar: 1, nombreEjemplar: 'albaca', monto: 5 }],
      },
      {
        razonSocial: 'edward ca',
        rif: '26529492-4',
        fechaDocumento: new Date().toJSON(),
        numeroCarrera: 55,
        nombreCliente: 'edward1 macorini',
        detalles: [{ numeroEjemplar: 1, nombreEjemplar: 'albaca', monto: 5 }],
      },
      {
        razonSocial: 'edward ca',
        rif: '26529492-4',
        fechaDocumento: new Date().toJSON(),
        numeroCarrera: 55,
        nombreCliente: 'edward macorini1',
        detalles: [{ numeroEjemplar: 1, nombreEjemplar: 'albaca', monto: 5 }],
      }
    ];
    ticket.forEach(x => {
      this.printerService.printDocument(x).subscribe((y) => {
        console.log(y);
      });
    })
    
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
