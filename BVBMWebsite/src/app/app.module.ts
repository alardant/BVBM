import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HeaderComponent } from './Components/Shared/header/header.component';
import { FooterComponent } from './Components/Shared/footer/footer.component';
import { ButtonComponent } from './Components/Shared/button/button.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AboutMeComponent } from './Components/about-me/about-me.component';
import { HomeComponent } from './Components/Homepage/home/home.component';
import { HomeOfferComponent } from './Components/Homepage/home-offer/home-offer.component';
import { OffersComponent } from './Components/Offerpage/offers/offers.component';
import { ContactComponent } from './Components/contact/contact.component';
import { OffersOfferComponent } from './Components/Offerpage/offers-offer/offers-offer.component';
import { ReviewsComponent } from './Components/Reviewpage/reviews/reviews.component';
import { LoginComponent } from './Components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtInterceptor } from './Interceptor/jwt.interceptor';
import { CookieService } from 'ngx-cookie-service';
import { CreateReviewComponent } from './Components/Reviewpage/create-review/create-review.component';
import { UpdateReviewComponent } from './Components/Reviewpage/update-review/update-review.component';
import { RecaptchaFormsModule, RecaptchaModule } from 'ng-recaptcha';
import { GoUpButtonComponent } from './Components/Shared/go-up-button/go-up-button.component';
import { CgdvComponent } from './Components/cgdv/cgdv.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    ButtonComponent,
    HomeOfferComponent,
    OffersComponent,
    AboutMeComponent,
    ContactComponent,
    HomeComponent,
    OffersOfferComponent,
    ReviewsComponent,
    LoginComponent,
    CreateReviewComponent,
    UpdateReviewComponent,
    GoUpButtonComponent,
    CgdvComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    RecaptchaModule,
    RecaptchaFormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    CookieService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
