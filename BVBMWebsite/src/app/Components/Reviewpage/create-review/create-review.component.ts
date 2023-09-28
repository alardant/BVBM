import { Component, ElementRef, Renderer2 } from '@angular/core';
import { Review } from '../../../Models/review';
import { Router } from '@angular/router';
import { ReviewService } from '../../../Services/Review/review.service';
import { Package } from '../../../Enum/packageEnum';
import { ViewportScroller } from '@angular/common';

@Component({
  selector: 'app-create-review',
  templateUrl: './create-review.component.html',
  styleUrls: ['./create-review.component.css']
})
export class CreateReviewComponent {
  review: Review = new Review();
  responseMessage: string = "";
  packageValues: Package[] = Object.values(Package).filter(value => typeof value === 'number') as Package[];

  constructor(private router: Router, private reviewService: ReviewService, private viewportScroller: ViewportScroller) { }

  redirectToListofReview() {
    this.router.navigate(['/reviews']);
  }

  createReview() {
    this.reviewService.CreateReview(this.review).subscribe(
      (result: string) => {
        this.responseMessage = "success";
        this.router.navigate(['/create-review']);
        this.review = new Review();
        this.viewportScroller.scrollToPosition([0, 0])
      },
      (error: string) => {
        this.responseMessage = "fail";
        this.viewportScroller.scrollToPosition([0, 0])
      }
    );
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
