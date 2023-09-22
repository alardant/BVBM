import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-offers-offer',
  templateUrl: './offers-offer.component.html',
  styleUrls: ['./offers-offer.component.css']
})
export class OffersOfferComponent {
  @Input() title!: string
  @Input() imageUrl!: string;
  @Input() imageAlt!: string;
  @Input() summary!: string;
  @Input() description!: string;
  @Input() price!: string;
  @Input() backgroundColor!: string;

  ngOnInit() { }
}
