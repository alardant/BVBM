import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutMeComponent } from './Components/about-me/about-me.component';
import { OffersComponent } from './Components/Offerpage/offers/offers.component';
import { ContactComponent } from './Components/contact/contact.component';
import { HomeComponent } from './Components/Homepage/home/home.component';

const routes: Routes = [
  { path: 'offres-et-tarifs', component: OffersComponent },
  { path: 'a-propos', component: AboutMeComponent },
  { path: 'contact', component: ContactComponent },
  { path: '', component: HomeComponent },
  { path: '**', component: HomeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
