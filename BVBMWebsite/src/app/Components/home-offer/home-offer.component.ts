import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-home-offer',
  templateUrl: './home-offer.component.html',
  styleUrls: ['./home-offer.component.css']
})

export class HomeOfferComponent {
  @Input()  imageUrl! : string;
  @Input()  imageAlt! : string;
  @Input()  title! : string;
  @Input()  description! : string;
  @Input()  backgroundColor! : string;

  ngOnInit() {

  }
}
