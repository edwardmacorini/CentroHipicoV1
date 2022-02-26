import { Cliente } from "./cliente";
import { Ejemplar } from "./ejemplar";

export interface Carrera {
    id: number;
    fechaCarrera: Date;
    estaActiva: boolean;
    estaCerrada: boolean;
    ganador: number | null;
    montoGanancia: number | null;
    montoSubTotal: number | null;
    montoTotal: number | null;
    ubicacion: string;
}

export interface CarreraResponse {
    carrera: Carrera;
    ejemplares: CarreraEjemplar[];
    detalles: CarreraDetalleResponse[];
}

export interface CarreraEjemplar {
    id: number;
    idCarrera: number;
    ejemplar: Ejemplar;
    esFavorito: boolean;

}

export interface CarreraDetalle {
    id: number;
    idCarrera: number;
    idCliente: number;
    idEjemplar: number;
    montoApuesta: number;
}

export interface CarreraDetalleResponse {
    id: number;
    idCarrera: number;
    cliente?: Cliente;
    ejemplar: CarreraEjemplar;
    montoApuesta?: number;
}

export interface CarreraGanador {
    idCarrera: number;
    idCliente: number;
}



