import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../Services/User/user.service';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isLoggedIn!: boolean;

  constructor(private userService: UserService, private cookieService: CookieService, private router: Router) { }

  ngOnInit() {
    this.userService.isAuthenticated$.subscribe((isAuthenticated: boolean) => {
      this.isLoggedIn = isAuthenticated;
    });
  }

  public Logout() {
    this.userService.Logout().subscribe(
      () => {
        this.cookieService.delete('auth_token', '/');
        this.router.navigate(['/']);
      },
      (error) => {
        this.router.navigate(['/']);
      }
    )
  }
}
