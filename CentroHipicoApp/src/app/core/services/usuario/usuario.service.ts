import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Usuario, UsuarioRequest } from '../../models/usuario';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private readonly ENDPOINT_API = `${environment.apiUrl}`;
  private readonly ENDPOINT_USUARIOS = `${this.ENDPOINT_API}/usuarios`;
  private readonly ENDPOINT_AUTENTICAR = `${this.ENDPOINT_USUARIOS}/autenticar`;
  private readonly ENDPOINT_REGISTRAR = `${this.ENDPOINT_USUARIOS}/registrar`;
  
  constructor(private httpClient: HttpClient) { }

  public autenticar(usuario: UsuarioRequest) {
    const body: any = usuario;
    return this.httpClient.post(this.ENDPOINT_AUTENTICAR, body);
  }

  public registrar(usuario: Usuario) {
    const body: any = usuario;
    return this.httpClient.post(this.ENDPOINT_REGISTRAR, body);
  }

}
