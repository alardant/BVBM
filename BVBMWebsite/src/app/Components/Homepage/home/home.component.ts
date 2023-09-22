import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Review } from '../../../Models/review';
import { ReviewService } from '../../../Services/Review/review.service';
import { Package } from '../../../Enum/packageEnum';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  reviews: Review[] = [];
  packages = Package;

  constructor(private router: Router, private reviewService: ReviewService) { }

  redirectToContact() {
    this.router.navigate(['/']);
  }

  ngOnInit(): void {
    this.reviewService.getReviews().subscribe((result: Review[]) => (this.reviews = result));
  }

   getPackageName(packageValue: Package): string {
    switch (packageValue) {
      case Package.ConsultationIndividuelle:
        return 'Consultation Individuelle';
      case Package.Pack3mois:
        return 'Pack 3 mois';
      case Package.ConsultationDomicile:
        return 'Consultation à domicile';
      default:
        return '';
    }
  }
}
