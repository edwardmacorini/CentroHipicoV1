import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgregarEjemplarComponent } from './agregar-ejemplar.component';

describe('AgregarEjemplarComponent', () => {
  let component: AgregarEjemplarComponent;
  let fixture: ComponentFixture<AgregarEjemplarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgregarEjemplarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgregarEjemplarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
