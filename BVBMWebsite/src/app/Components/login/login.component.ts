import { Component } from '@angular/core';
import { UserService } from '../../Services/User/user.service';
import { User } from '../../Models/user';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  user:  User = { email: '', password: '' };;
  responseMessage: string = "";

  constructor(private router: Router, private userService: UserService, private cookieService: CookieService) { }

  login() {
    console.log(this.user);
    this.userService.Login(this.user).subscribe(
      (token: string) => {

        const expirationHours = 1;
        const expirationDate = new Date();
        expirationDate.setTime(expirationDate.getTime() + expirationHours * 60 * 60 * 1000);

        this.cookieService.set('auth_token', token, expirationDate, '/', undefined, true, 'Lax');
        this.router.navigate(['/']);
      },
      (error: string) => {
        this.responseMessage = error;
        this.responseMessage = "Échec de l'authentification, veuillez réessayer";
      }
    );
  }

}
