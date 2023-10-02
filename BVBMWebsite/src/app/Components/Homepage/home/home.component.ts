import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Review } from '../../../Models/review';
import { ReviewService } from '../../../Services/Review/review.service';
import { Package } from '../../../Enum/packageEnum';
import { UserService } from '../../../Services/User/user.service';
import { ViewportScroller } from '@angular/common';
import { Meta, Title } from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  reviews: Review[] = [];
  packages = Package;

  constructor(private router: Router,
    private reviewService: ReviewService,
    private userService: UserService,
    private viewPortScroller: ViewportScroller,
    private titleService: Title,
    private meta: Meta) { }

  redirectToContact() {
    this.router.navigate(['/contact']);
  }

  redirectToOffers() {
    this.router.navigate(['/offres-et-tarifs']);
  }

  ngOnInit() {
    this.titleService.setTitle('Bien Vivre Bien Manger | Accompagnement en alimentation végétale');
    this.meta.addTag({
      name: 'description', content: 'Avec Bien Vivre Bien Manger, recevez des conseils presonnalisés pour un mode de vie sain et durable à travers alimentation végétale équilibrée.' });

    this.reviewService.GetAllReviews().subscribe((result: Review[]) => (this.reviews = result));
  }

  getPackageName(packageValue: Package): string {
    switch (packageValue) {
      case Package.ConsultationIndividuelle:
        return 'Consultation Individuelle';
      case Package.Pack3mois:
        return 'Pack 3 mois';
      case Package.ConsultationDomicile:
        return 'Consultation à domicile';
      default:
        return '';
    }
  }
}
