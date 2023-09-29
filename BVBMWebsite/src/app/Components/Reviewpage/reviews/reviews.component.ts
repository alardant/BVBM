import { Component, OnInit } from '@angular/core';
import { Review } from '../../../Models/review';
import { ReviewService } from '../../../Services/Review/review.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})
export class ReviewsComponent implements OnInit {
  reviews: Review[] = [];
  responseMessage: string = "";

  constructor(private router: Router, private reviewService: ReviewService) { }

  redirectToCreateReview() {
    this.router.navigate(['/create-review']);
  }

  redirectToUpdateReview(id: number) {
    this.router.navigate([`/update-review/${id}`])
  }

  ngOnInit(): void {
    this.reviewService.GetAllReviews().subscribe((result: Review[]) => (this.reviews = result));
  }

}
