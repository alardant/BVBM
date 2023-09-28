import { Component } from '@angular/core';
import { Review } from '../../../Models/review';
import { Router } from '@angular/router';
import { ReviewService } from '../../../Services/Review/review.service';

@Component({
  selector: 'app-create-review',
  templateUrl: './create-review.component.html',
  styleUrls: ['./create-review.component.css']
})
export class CreateReviewComponent {
  review!: Review;
  responseMessage: string = "";

  constructor(private router: Router, private reviewService: ReviewService) { }

  redirectToListofReview() {
    this.router.navigate(['/reviews']);
  }

  ngOnInit(): void {
    this.reviewService.CreateReview().subscribe((result: string) => (this.responseMessage = result));
  }
}
