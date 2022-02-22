
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cliente } from '../../models/cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  private readonly ENDPOINT_API = `${environment.apiUrl}`;
  private readonly ENDPOINT_CLIENTES = `${this.ENDPOINT_API}/clientes`;

  constructor(private httpClient: HttpClient) { }

  public obtenerClientes(): Observable<Cliente[]> {
    return this.httpClient.get<Cliente[]>(this.ENDPOINT_CLIENTES);
  }

  public obtenerCliente(idCliente: number): Observable<Cliente> {
    const httpParams = new HttpParams()
      .append('idCarrera', idCliente.toString());
    return this.httpClient.get<Cliente>(this.ENDPOINT_CLIENTES, { params: httpParams });
  }

  public agregarCliente(cliente: Cliente): Observable<void> {
    const body: any = cliente;
    return this.httpClient.post<void>(this.ENDPOINT_CLIENTES, body);
  }

  public actualizarCliente(cliente: Cliente): Observable<void> {
    const body: any = cliente;
    return this.httpClient.put<void>(this.ENDPOINT_CLIENTES, body);
  }

  public eliminarCliente(idCliente: Cliente): Observable<void> {
    const httpParams = new HttpParams()
      .append('idCliente', idCliente.toString());
    return this.httpClient.delete<void>(this.ENDPOINT_CLIENTES, { params: httpParams });
  }
}
