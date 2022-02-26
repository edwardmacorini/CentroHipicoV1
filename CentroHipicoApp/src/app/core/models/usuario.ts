export interface Usuario extends UsuarioRequest {
    id: number | undefined;
    name: string;
}

export interface UsuarioRequest {
    userName: string;
    password: string;
}