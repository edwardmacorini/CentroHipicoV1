import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EliminarCarreraComponent } from './eliminar-carrera.component';

describe('EliminarCarreraComponent', () => {
  let component: EliminarCarreraComponent;
  let fixture: ComponentFixture<EliminarCarreraComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EliminarCarreraComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EliminarCarreraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
