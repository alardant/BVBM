import { Injectable } from '@angular/core';
import { Review } from '../../Models/review';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  //Tochange when deployed
  private apiurl = "https://localhost:7267/api/Review"
  private urlCreateReview = "Create"
  private urlUpdateReview = "Update"
  private urlDeleteReview = "Delete"


  constructor(private http: HttpClient) { }

  public GetAllReviews(): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.apiurl}`);
  }

  public CreateReview(review: Review): Observable<string> {
    console.log(review);
    return this.http.post(`${this.apiurl}/${this.urlCreateReview}`, review, { responseType: 'text' });
  }

  public UdpateReview(reviewId: number): Observable<string> {
    return this.http.put(`${this.apiurl}/${this.urlUpdateReview}/${reviewId}`, reviewId,  { responseType: 'text' });
  }

  public DeleteReview(reviewId: number): Observable<string> {
    return this.http.delete(`${this.apiurl}/${this.urlDeleteReview}/${reviewId}`, { responseType: 'text' });
  }
}
