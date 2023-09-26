import { Injectable } from '@angular/core';
import { Review } from '../../Models/review';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  //Tochange when deployed
  private apiurl = "https://localhost:7267/api"
  private urlAllReviews = "Review"
  private urlReviewsValidated = "Review/Validated"
  private urlReviewsNotValidated = "Review/NotValidated"
  private urlValidate = "Review/Validate";
  private urlUnvalidate = "Review/UnvalidateReview";

  constructor(private http: HttpClient) { }

  public getReviews(): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.apiurl}/${this.urlAllReviews}`);
  }

  public getReviewsValidated(): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.apiurl}/${this.urlReviewsValidated}`);
  }

  public getReviewsNotValidated(): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.apiurl}/${this.urlReviewsNotValidated}`);
  }

  public validateReview(reviewId: number): Observable<string> {
    return this.http.put(`${this.apiurl}/${this.urlValidate}/${reviewId}`, {}, { responseType: 'text' });
  }

  public unvalidateReview(reviewId: number): Observable<string> {
    return this.http.put(`${this.apiurl}/${this.urlUnvalidate}/${reviewId}`, {}, { responseType: 'text' });
  }
}
