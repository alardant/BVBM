import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CgdvComponent } from './cgdv.component';

describe('CgdvComponent', () => {
  let component: CgdvComponent;
  let fixture: ComponentFixture<CgdvComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CgdvComponent]
    });
    fixture = TestBed.createComponent(CgdvComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
