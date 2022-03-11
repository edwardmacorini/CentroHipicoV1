import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Cliente } from 'src/app/core/models/cliente';
import { ClienteService } from 'src/app/core/services/cliente/cliente.service';

@Component({
  templateUrl: './agregar-cliente.component.html',
  styleUrls: ['./agregar-cliente.component.scss']
})
export class AgregarClienteComponent {

  clienteForm = new FormGroup({
    nombre: new FormControl('', [Validators.required]),
    telefono: new FormControl('')
  });

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<AgregarClienteComponent>,
    private clienteService: ClienteService
  ) { }

  onSubmit(event: Event) {
    event.preventDefault();
    if(this.clienteForm.valid) {
      let cliente: Cliente = {
        id: undefined,
        nombre: this.clienteForm.get('nombre')?.value,
        telefono: this.clienteForm.get('telefono')?.value
      };
      this.clienteService.agregarCliente(cliente)
        .subscribe(() => {
          this.dialogRef.close();
        });
    }
  }

  onClose(event: Event): void {
    event.preventDefault();
    this.dialogRef.close();
  }

}
