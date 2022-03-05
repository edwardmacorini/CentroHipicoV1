import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Usuario } from 'src/app/core/models/usuario';
import { UsuarioService } from 'src/app/core/services/usuario/usuario.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  usuarioForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(3)]),
    username: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
    ]),
  });

  constructor(private usuarioService: UsuarioService) {}

  ngOnInit(): void {}

  onRegistrarUsuario() {
    console.log('prueba');

    if (this.usuarioForm.valid) {
      let usuario: Usuario = {
        id: undefined,
        name: this.usuarioForm.get('name')?.value,
        username: this.usuarioForm.get('username')?.value,
        password: this.usuarioForm.get('password')?.value,
      };
      this.usuarioService.register(usuario).subscribe({
        next: (res) => {
          console.log(res);
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }
}
