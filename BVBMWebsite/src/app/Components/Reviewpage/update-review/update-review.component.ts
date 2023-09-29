import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../../../Services/Review/review.service';
import { HttpClient } from '@angular/common/http';
import { Review } from '../../../Models/review';
import { ActivatedRoute, Router } from '@angular/router';
import { Package } from '../../../Enum/packageEnum';

@Component({
  selector: 'app-update-review',
  templateUrl: './update-review.component.html',
  styleUrls: ['./update-review.component.css']
})
export class UpdateReviewComponent implements OnInit {
  review: Review = new Review();
  responseMessage: string = "";
  packageValues: Package[] = Object.values(Package).filter(value => typeof value === 'number') as Package[];
  nameError: string = '';
  packageError: string = '';
  descriptionError: string = '';

  constructor(private reviewService: ReviewService, private hhtp: HttpClient, private activatedRoute: ActivatedRoute, private router: Router) {
    
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params) => {
      if (params['id']) {
        this.reviewService.GetReviewbyId(params['id']).subscribe(
          (result) => {
            console.log(result)
            this.review = result
          },
          (error) => {
          }
        );
      }
    })
  }

  updateReview() {
    // Réinitialise les messages d'erreur avant chaque soumission
    this.nameError = '';
    this.packageError = '';
    this.descriptionError = '';

    // Valide des champs et affichage des messages d'erreur si nécessaire
    if (!this.review.name) {
      this.nameError = 'Le champ Nom est requis.';
    }
    if (!this.review.package) {
      this.packageError = 'Le champ Package est requis.';
    }
    if (!this.review.description) {
      this.descriptionError = 'Le champ Description est requis.';
    }

    // Vérifie s'il y a des erreurs avant de soumettre le formulaire
    if (!this.nameError && !this.packageError && !this.descriptionError) {
      // Soumets le formulaire
      this.reviewService.UdpateReview(this.review.id, this.review).subscribe(
        (result: string) => {
          this.responseMessage = 'success';
          this.router.navigate(['/update-review/:id}']);
        },
        (error: string) => {
          this.responseMessage = 'fail';
        }
      );
    }
  }

  redirectToListofReview() {
    this.router.navigate(['/reviews']);
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
