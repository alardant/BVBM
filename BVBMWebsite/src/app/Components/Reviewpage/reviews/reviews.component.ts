import { Component, OnInit } from '@angular/core';
import { Review } from '../../../Models/review';
import { ReviewService } from '../../../Services/Review/review.service';
import { Router } from '@angular/router';
import { ViewportScroller } from '@angular/common';
import { Package } from '../../../Enum/packageEnum';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})
export class ReviewsComponent implements OnInit {
  reviews: Review[] = [];
  responseMessage: string = "";

  constructor(private router: Router, private reviewService: ReviewService, private viewportScroller: ViewportScroller) { }

  redirectToCreateReview() {
    this.router.navigate(['/create-review']);
  }

  redirectToUpdateReview(id: number) {
    this.router.navigate([`/update-review/${id}`])
  }

  deleteReview(id: number) {
    this.reviewService.DeleteReview(id).subscribe(
      (result) => {
        this.responseMessage = 'success';
        this.refreshReviewsList();
        this.viewportScroller.scrollToPosition([0, 0]);
      },
      (error) => {
        this.responseMessage = 'fail';
        this.viewportScroller.scrollToPosition([0, 0]);
      }
    )
  }

  ngOnInit(): void {
    this.reviewService.GetAllReviews().subscribe((result: Review[]) => (this.reviews = result));
  }

  private refreshReviewsList() {
    this.reviewService.GetAllReviews().subscribe((result: Review[]) => {
      this.reviews = result;
    });
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
