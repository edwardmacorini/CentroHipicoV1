import { Injectable } from "@angular/core";
import { BaseState } from "./base.state";

@Injectable({
    providedIn: 'root',
})
export class CarreraState extends BaseState<any | null> {
    constructor() {
        super(null);
    }
}