import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OffersOfferComponent } from './offers-offer.component';

describe('OffersOfferComponent', () => {
  let component: OffersOfferComponent;
  let fixture: ComponentFixture<OffersOfferComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OffersOfferComponent]
    });
    fixture = TestBed.createComponent(OffersOfferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
