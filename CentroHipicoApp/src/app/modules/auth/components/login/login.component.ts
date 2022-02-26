import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UsuarioRequest } from 'src/app/core/models/usuario';
import { UsuarioService } from 'src/app/core/services/usuario/usuario.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  usuarioForm = new FormGroup({
    userName: new FormControl('', [Validators.required, Validators.minLength(3)]),
    password: new FormControl('', [Validators.required, Validators.minLength(3)]),
  })
  constructor(private usuarioService: UsuarioService) { }

  ngOnInit(): void {
  }

  onAutenticarse() {
    console.log('prueba');
    
    if (this.usuarioForm.valid) {
      let usuario: UsuarioRequest = {
        userName: this.usuarioForm.get('userName')?.value,
        password: this.usuarioForm.get('password')?.value
      }
      this.usuarioService.autenticar(usuario)
        .subscribe({
          next: (res) => {
            console.log(res);
          },
          error: (err) => {
            console.log(err);
          }
        });
    }
  }

}
