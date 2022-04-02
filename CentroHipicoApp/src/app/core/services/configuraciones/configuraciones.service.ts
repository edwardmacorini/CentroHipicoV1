import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ConfiguracionesService {
  private readonly ENDPOINT_API = `${environment.apiUrl}`;

  constructor(private httpClient: HttpClient) {}

  public getImpresoraIp() {
    return localStorage.getItem('ipconfig');
  }

  public setImpresoraIp(ip: string) {
    localStorage.removeItem('ipconfig');
    localStorage.setItem('ipconfig', ip.trim());
  }
}
