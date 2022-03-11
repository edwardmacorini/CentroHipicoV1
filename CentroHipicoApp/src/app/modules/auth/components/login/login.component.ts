import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { UsuarioRequest } from 'src/app/core/models/usuario';
import { UsuarioService } from 'src/app/core/services/usuario/usuario.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  usuarioForm = new FormGroup({
    username: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
    ]),
  });

  constructor(
    private usuarioService: UsuarioService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {}

  onLogin() {
    console.log('prueba');

    if (this.usuarioForm.valid) {
      let usuario: UsuarioRequest = {
        username: this.usuarioForm.get('username')?.value,
        password: this.usuarioForm.get('password')?.value,
      };
      this.usuarioService.login(usuario).subscribe({
        next: (res) => {
          console.log(res);
          this.router.navigate(['home']);
        },
        error: (err) => {
          console.warn(err);
          if (err.status == 403)
            this.snackBar.open('Usuario/Contraseña invalidos', 'X', {
              duration: 2 * 2000,
            });
          else
            this.snackBar.open('Error en conexión con el servidor', 'X', {
              duration: 2 * 2000,
            });
        },
      });
    }
  }
}
