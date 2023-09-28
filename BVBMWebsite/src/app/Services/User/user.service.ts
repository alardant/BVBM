import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../../Models/user';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiurl = "https://localhost:7267/api/Auth";
  private urlLogin = "Login";
  private urlLoggedIn = "LoggedIn";
  private urlLogout = "Logout";

  private isAuthenticatedSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public isAuthenticated$: Observable<boolean> = this.isAuthenticatedSubject.asObservable();

  constructor(private http: HttpClient) {
    // Initialisez l'état de connexion lors de la construction du service
    this.checkLoginStatus();
  }

  public Login(user: User): Observable<string> {
    return this.http.post(`${this.apiurl}/${this.urlLogin}`, user, { responseType: 'text' })
      .pipe(
        tap(() => {
          this.setAuthenticationStatus(true);
        })
      );
  }

  public Logout(): Observable<any> {
    return this.http.post(`${this.apiurl}/${this.urlLogout}`, {})
      .pipe(
        tap(() => {
          this.setAuthenticationStatus(false);
        })
      );
  }

  private checkLoginStatus() {
    // Utilisez la méthode isLoggedIn pour vérifier l'état de connexion actuel
    this.isLoggedIn().subscribe((loggedIn: boolean) => {
      this.isAuthenticatedSubject.next(loggedIn);
    });
  }

  public isLoggedIn(): Observable<boolean> {
    return this.http.get(`${this.apiurl}/${this.urlLoggedIn}`).pipe(
      map((response: any) => {
        return response as boolean;
      })
    );
  }

  setAuthenticationStatus(isAuthenticated: boolean) {
    this.isAuthenticatedSubject.next(isAuthenticated);
  }
}
