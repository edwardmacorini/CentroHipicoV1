import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AbrirCerrarCarreraComponent } from './abrir-cerrar-carrera.component';

describe('AbrirCerrarCarreraComponent', () => {
  let component: AbrirCerrarCarreraComponent;
  let fixture: ComponentFixture<AbrirCerrarCarreraComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AbrirCerrarCarreraComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AbrirCerrarCarreraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
