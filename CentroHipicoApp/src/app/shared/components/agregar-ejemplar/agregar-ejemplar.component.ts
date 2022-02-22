import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Ejemplar } from 'src/app/core/models/ejemplar';
import { EjemplarService } from 'src/app/core/services/ejemplar/ejemplar.service';

@Component({
  selector: 'app-agregar-ejemplar',
  templateUrl: './agregar-ejemplar.component.html',
  styleUrls: ['./agregar-ejemplar.component.scss']
})
export class AgregarEjemplarComponent implements OnInit {

  ejemplarForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<AgregarEjemplarComponent>,
    private fb: FormBuilder,
    private ejemplarService: EjemplarService
  ) {
    this.ejemplarForm = this.fb.group({
      nombre: ['', [Validators.required, Validators.minLength(3)]]
    });
  }

  ngOnInit(): void {
  }

  onSubmit(event: Event) {
    event.preventDefault();
    if (this.ejemplarForm.valid) {
      let ejemplar: Ejemplar = {
        id: 0,
        nombre: this.ejemplarForm.get('nombre')?.value
      }
      this.ejemplarService.agregarEjemplar(ejemplar)
        .subscribe(() => this.dialogRef.close());
    }
  }

  onClose(event: Event): void {
    event.preventDefault();
    this.dialogRef.close();
  }

}
