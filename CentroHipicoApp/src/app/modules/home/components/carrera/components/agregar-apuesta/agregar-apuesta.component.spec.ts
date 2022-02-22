import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgregarApuestaComponent } from './agregar-apuesta.component';

describe('AgregarApuestaComponent', () => {
  let component: AgregarApuestaComponent;
  let fixture: ComponentFixture<AgregarApuestaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgregarApuestaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgregarApuestaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
