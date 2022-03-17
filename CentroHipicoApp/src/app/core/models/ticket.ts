export interface Ticket {
  razonSocial: string;
  rif: string;
  fechaDocumento: string;
  numeroCarrera: number;
  nombreCliente: string;
  detalles: TicketDetalle[];
}

export interface TicketDetalle {
  numeroEjemplar: number;
  nombreEjemplar: string;
  monto: number;
}
