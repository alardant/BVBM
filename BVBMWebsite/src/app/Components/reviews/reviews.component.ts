import { Component } from '@angular/core';
import { Review } from '../../Models/review';
import { ReviewService } from '../../Services/Review/review.service';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})
export class ReviewsComponent {
  validatedReviews: Review[] = [];
  unvalidatedReviews: Review[] = [];
  responseMessage: string = "";

  constructor(private reviewService: ReviewService) { }

  ngOnInit(): void {
    this.reviewService.getReviewsValidated().subscribe((result: Review[]) => (this.validatedReviews = result));
    this.reviewService.getReviewsNotValidated().subscribe((result: Review[]) => (this.unvalidatedReviews = result));
  }

  validateReview(review: Review) {
    this.reviewService.validateReview(review.id).subscribe((response: string) => {
      if (response) {
        // Stocke la réposne dans une variable de composant pour l'afficher dans la vue
        this.responseMessage = response;
      }
      // Met à jour la liste des reviews non-validés    
      this.unvalidatedReviews = this.unvalidatedReviews.filter(r => r !== review);
      // Met à jour la liste des reviews validées
      this.validatedReviews.push(review);
    });
  }
}
