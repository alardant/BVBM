import { Injectable } from '@angular/core';
import { HttpClient,  } from '@angular/common/http';
import { User } from '../../Models/user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  private apiurl = "https://localhost:7267/api/Auth"
  private urlLogin ="Login"

  constructor(private http: HttpClient) { }

  public Login(user: User): Observable<string> {
    return this.http.post(`${this.apiurl}/${this.urlLogin}`, user, { responseType: 'text' });
  }


}
