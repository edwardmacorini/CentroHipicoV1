import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfiguracionesService } from 'src/app/core/services/configuraciones/configuraciones.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})
export class SettingsComponent implements OnInit {
  ipConfigForm = new FormGroup({
    ip: new FormControl('', [Validators.required]),
  });

  constructor(
    private _configuraciones: ConfiguracionesService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.ipConfigForm.get('ip')?.setValue(this._configuraciones.getImpresoraIp());
  }

  onGuardarImpresoraIp() {
    if (this.ipConfigForm.valid) {
      this._configuraciones.setImpresoraIp(this.ipConfigForm.value.ip);
      this.snackBar.open('Configuración guardada con exito.', 'X', {
        duration: 2 * 1000,
      });
    }
  }
}
