import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Usuario, UsuarioRequest, UsuarioResponse } from '../../models/usuario';
import { UsuarioState } from '../../states/usuario.state';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private readonly ENDPOINT_API = `${environment.apiUrl}`;
  private readonly ENDPOINT_USUARIOS = `${this.ENDPOINT_API}/usuarios`;
  private readonly ENDPOINT_AUTENTICAR = `${this.ENDPOINT_USUARIOS}/autenticar`;
  private readonly ENDPOINT_REGISTRAR = `${this.ENDPOINT_USUARIOS}/registrar`;

  get usuario$(): Observable<Usuario | null> { return this.usuarioState.state$; }
  set setUsuarioState(value: any) { this.usuarioState.setState = value; }
  
  constructor(
    private httpClient: HttpClient,
    private jwtHelper: JwtHelperService,
    private usuarioState: UsuarioState,
    private router: Router) { }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('access_token');
    return !this.jwtHelper.isTokenExpired(token ?? '');
  }

  public getToken(): string | null {
    return localStorage.getItem('access_token');
  }

  public login(usuario: UsuarioRequest): Observable<UsuarioResponse> {
    const body: any = usuario;
    return this.httpClient.post<UsuarioResponse>(this.ENDPOINT_AUTENTICAR, body)
      .pipe(
        map((val) => {
          localStorage.setItem('user', JSON.stringify(val));
          localStorage.setItem('access_token', val.token)
          this.setUsuarioState = {
            id: val.id,
            name: val.name,
            username: val.username,
          };
          return val || {};
        })
      );
  }

  public register(usuario: Usuario): Observable<void> {
    const body: any = usuario;
    return this.httpClient.post<void>(this.ENDPOINT_REGISTRAR, body);
  }

  public logout(): void {
    this.usuarioState.setState = null;
    localStorage.clear();
    this.router.navigate(['/']);
  }

}
