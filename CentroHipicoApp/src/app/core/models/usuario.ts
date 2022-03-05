export interface UsuarioRequest {
    username: string;
    password: string | undefined;
}

export interface Usuario extends UsuarioRequest {
    id: number | undefined;
    name: string;
}

export interface UsuarioResponse extends Usuario  {
    token: string;
}