import { BehaviorSubject, Observable } from "rxjs";

export abstract class BaseState<T> {

    state$: Observable<T>;

    private _state$: BehaviorSubject<T>;

    get state(): T {
        return this._state$.getValue();
    }

    set setState(nextState: T) {
        this._state$.next(nextState);
    }

    protected constructor(initialState: T) {
        this._state$ = new BehaviorSubject<T>(initialState);
        this.state$ = this._state$.asObservable();
    }

}