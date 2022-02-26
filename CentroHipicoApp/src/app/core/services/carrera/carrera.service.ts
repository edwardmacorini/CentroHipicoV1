import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Carrera, CarreraDetalle, CarreraEjemplar, CarreraGanador, CarreraResponse } from '../../models/carrera';
import { CarreraState } from '../../states/carrera.state';

@Injectable({
  providedIn: 'root'
})
export class CarreraService {
  private readonly ENDPOINT_API = `${environment.apiUrl}`;
  private readonly ENDPOINT_CARRERAS = `${this.ENDPOINT_API}/carreras`;
  private readonly ENDPOINT_EJEMPLAR = `${this.ENDPOINT_CARRERAS}/ejemplar`;
  private readonly ENDPOINT_APUESTA = `${this.ENDPOINT_CARRERAS}/apuesta`;
  private readonly ENDPOINT_GANADOR = `${this.ENDPOINT_CARRERAS}/ganador`;

  get carrera$(): Observable<CarreraResponse | null> { return this.carreraState.state$; }
  set setCarreraState(value: any) { this.carreraState.setState = value; }

  constructor(
    private httpClient: HttpClient,
    private carreraState: CarreraState
  ) { }

  public obtenerCarreras(): Observable<Carrera[]> {
    return this.httpClient.get<Carrera[]>(`${this.ENDPOINT_CARRERAS}`);
  }

  public obtenerCarrera(idCarrera: number): Observable<CarreraResponse> {
    return this.httpClient.get<CarreraResponse>(`${this.ENDPOINT_CARRERAS}/${idCarrera.toString()}`);
  }

  public refrescarCarrera(idCarrera: number): void {
    this.obtenerCarrera(idCarrera).subscribe((response) => this.setCarreraState = response);
  }

  public agregarCarrera(carrera: Carrera): Observable<any> {
    const body: any = carrera;
    return this.httpClient.post(`${this.ENDPOINT_CARRERAS}/agregar`, body);
  }

  public eliminarCarrera(carrera: Carrera): Observable<any> {
    const body: any = carrera;
    return this.httpClient.post(`${this.ENDPOINT_CARRERAS}/eliminar`, body);
  }

  public abrirCarrera(id: number): Observable<any> {
    const httpParams = new HttpParams()
      .append('idCarrera', id.toString());
    return this.httpClient.post(`${this.ENDPOINT_CARRERAS}/abrir`, null, { params: httpParams });
  }

  public cerrarCarrera(id: number): Observable<any> {
    const httpParams = new HttpParams()
      .append('idCarrera', id.toString());
    return this.httpClient.post(`${this.ENDPOINT_CARRERAS}/cerrar`, null, { params: httpParams });
  }

  public agregarApuesta(carreraDetalle: CarreraDetalle): Observable<any> {
    const body: any = carreraDetalle;
    return this.httpClient.post(`${this.ENDPOINT_APUESTA}/agregar`, body);
  }

  public eliminarApuesta(carreraDetalle: CarreraDetalle): Observable<any> {
    const body: any = carreraDetalle;
    return this.httpClient.post(`${this.ENDPOINT_APUESTA}/eliminar`, body);
  }

  public agregarEjemplar(carreraEjemplar: CarreraEjemplar) {
    const body: any = carreraEjemplar;
    return this.httpClient.post(`${this.ENDPOINT_EJEMPLAR}/agregar`, body);
  }

  public eliminarEjemplar(carreraEjemplar: CarreraEjemplar) {
    const body: any = carreraEjemplar;
    return this.httpClient.post(`${this.ENDPOINT_EJEMPLAR}/eliminar`, body);
  }

  public agregarGanador(carreraGanador: CarreraGanador) {
    const body: any = carreraGanador;
    return this.httpClient.post(`${this.ENDPOINT_GANADOR}`, body);
  }
}
