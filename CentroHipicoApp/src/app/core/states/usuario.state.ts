import { Injectable } from "@angular/core";
import { Usuario } from "../models/usuario";
import { BaseState } from "./base.state";

@Injectable({
    providedIn: 'root',
})
export class UsuarioState extends BaseState<Usuario | null> {
    constructor() {
        super(null);
    }
}