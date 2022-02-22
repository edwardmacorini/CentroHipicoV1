import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Ejemplar } from '../../models/ejemplar';
@Injectable({
  providedIn: 'root'
})
export class EjemplarService {
  private readonly ENDPOINT_API = `${environment.apiUrl}`;
  private readonly ENDPOINT_EJEMPLARES = `${this.ENDPOINT_API}/ejemplares`;

  constructor(private httpClient: HttpClient) { }

  public obtenerEjemplares(): Observable<Ejemplar[]> {
    return this.httpClient.get<Ejemplar[]>(this.ENDPOINT_EJEMPLARES);
  }

  public obtenerEjemplar(idEjemplar: number): Observable<Ejemplar> {
    const httpParams = new HttpParams()
      .append('idCarrera', idEjemplar.toString());
    return this.httpClient.get<Ejemplar>(this.ENDPOINT_EJEMPLARES, { params: httpParams });
  }

  public agregarEjemplar(Ejemplar: Ejemplar): Observable<void> {
    const body: any = Ejemplar;
    return this.httpClient.post<void>(this.ENDPOINT_EJEMPLARES, body);
  }

  public actualizarEjemplar(Ejemplar: Ejemplar): Observable<void> {
    const body: any = Ejemplar;
    return this.httpClient.put<void>(this.ENDPOINT_EJEMPLARES, body);
  }

  public eliminarEjemplar(idEjemplar: Ejemplar): Observable<void> {
    const httpParams = new HttpParams()
      .append('idEjemplar', idEjemplar.toString());
    return this.httpClient.delete<void>(this.ENDPOINT_EJEMPLARES, { params: httpParams });
  }

}
