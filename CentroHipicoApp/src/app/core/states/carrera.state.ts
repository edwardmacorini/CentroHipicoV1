import { Injectable } from "@angular/core";
import { CarreraResponse } from "../models/carrera";
import { BaseState } from "./base.state";

@Injectable({
    providedIn: 'root',
})
export class CarreraState extends BaseState<CarreraResponse | null> {
    constructor() {
        super(null);
    }
}