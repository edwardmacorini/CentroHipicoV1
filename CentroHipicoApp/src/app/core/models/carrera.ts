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

export interface carreraResponse {
    carrera: Carrera;
    ejemplares: CarreraEjemplar[];
    detalles: CarreraDetalle[];
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
    cliente?: Cliente;
    ejemplar: CarreraEjemplar;
    montoApuesta?: number;
}

export interface CarreraGanador {
    idCarrera: number;
    idCliente: number;
}



