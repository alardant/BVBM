import { Injectable } from '@angular/core';
import { Review } from '../../Models/review';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  private apiurl = "https://localhost:7267/api"
  private url = "Review"
  constructor(private http: HttpClient) { }

  public getReviews(): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.apiurl}/${this.url}`)
;  }
}
