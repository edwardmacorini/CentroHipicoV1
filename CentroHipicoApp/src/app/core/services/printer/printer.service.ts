import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Ticket } from '../../models/ticket';

@Injectable({
  providedIn: 'root',
})
export class PrinterService {
  private readonly ENDPOINT_API = `${environment.printerUrl}`;
  private readonly ENDPOINT_PRINT = `${this.ENDPOINT_API}/print`;

  constructor(private httpClient: HttpClient) {}

  public printDocument(ticket: Ticket) {
    let body: any = ticket;
    return this.httpClient.post(this.ENDPOINT_PRINT, body);
  }
}
