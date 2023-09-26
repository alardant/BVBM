import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HeaderComponent } from './Components/Shared/header/header.component';
import { FooterComponent } from './Components/Shared/footer/footer.component';
import { ButtonComponent } from './Components/Shared/button/button.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { AboutMeComponent } from './Components/about-me/about-me.component';
import { HomeComponent } from './Components/Homepage/home/home.component';
import { HomeOfferComponent } from './Components/Homepage/home-offer/home-offer.component';
import { OffersComponent } from './Components/Offerpage/offers/offers.component';
import { ContactComponent } from './Components/contact/contact.component';
import { OffersOfferComponent } from './Components/Offerpage/offers-offer/offers-offer.component';
import { ReviewsComponent } from './Components/reviews/reviews.component';


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
    ReviewsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
