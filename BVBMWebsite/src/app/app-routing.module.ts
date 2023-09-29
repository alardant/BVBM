import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutMeComponent } from './Components/about-me/about-me.component';
import { OffersComponent } from './Components/Offerpage/offers/offers.component';
import { ContactComponent } from './Components/contact/contact.component';
import { HomeComponent } from './Components/Homepage/home/home.component';
import { ReviewsComponent } from './Components/Reviewpage/reviews/reviews.component';
import { CreateReviewComponent } from './Components/Reviewpage/create-review/create-review.component';
import { UpdateReviewComponent } from './Components/Reviewpage/update-review/update-review.component';
import { LoginComponent } from './Components/login/login.component';
import { AuthGuard } from './Guard/auth.guard';

const routes: Routes = [
  { path: 'offres-et-tarifs', component: OffersComponent },
  { path: 'login', component: LoginComponent },
  { path: 'a-propos', component: AboutMeComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'reviews', component: ReviewsComponent, canActivate: [AuthGuard] },
  { path: 'create-review', component: CreateReviewComponent, canActivate: [AuthGuard] },
  { path: 'update-review/:id', component: UpdateReviewComponent, canActivate: [AuthGuard] },
  { path: '', component: HomeComponent },
  { path: '**', component: HomeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
