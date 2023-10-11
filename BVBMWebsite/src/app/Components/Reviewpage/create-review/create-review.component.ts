import { Component, ElementRef, Renderer2 } from '@angular/core';
import { Review } from '../../../Models/review';
import { Router } from '@angular/router';
import { ReviewService } from '../../../Services/Review/review.service';
import { Package } from '../../../Enum/packageEnum';
import { ViewportScroller } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-review',
  templateUrl: './create-review.component.html',
  styleUrls: ['./create-review.component.css']
})
export class CreateReviewComponent {
  review: Review = new Review();
  responseMessage: string = "";
  packageValues: Package[] = Object.values(Package).filter(value => typeof value === 'number') as Package[];
  nameError: string = '';
  descriptionError: string = '';

  constructor(private router: Router, private reviewService: ReviewService, private viewportScroller: ViewportScroller) {}

  redirectToListofReview() {
    this.router.navigate(['/reviews']);
  }

  createReview() {
    // Réinitialise les messages d'erreur avant chaque soumission
    this.nameError = '';
    this.descriptionError = '';

    // Valide des champs et affichage des messages d'erreur si nécessaire
    if (!this.review.name) {
      this.nameError = 'Le champ Nom est requis.';
    }
    if (!this.review.description) {
      this.descriptionError = 'Le champ Description est requis.';
    }

    // Vérifie s'il y a des erreurs avant de soumettre le formulaire
    if (!this.nameError && !this.descriptionError) {
      // Soumets le formulaire
      this.reviewService.CreateReview(this.review).subscribe(
        (result: string) => {
          this.responseMessage = 'success';
          this.router.navigate(['/create-review']);
          this.review = new Review();
        },
        (error: string) => {
          this.responseMessage = 'fail';
         this.viewportScroller.scrollToPosition([0, 0]);
        }
      );
    }
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
