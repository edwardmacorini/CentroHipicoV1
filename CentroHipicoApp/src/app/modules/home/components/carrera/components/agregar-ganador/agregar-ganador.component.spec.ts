import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgregarGanadorComponent } from './agregar-ganador.component';

describe('AgregarGanadorComponent', () => {
  let component: AgregarGanadorComponent;
  let fixture: ComponentFixture<AgregarGanadorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgregarGanadorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgregarGanadorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
