import { Component } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-offers',
  templateUrl: './offers.component.html',
  styleUrls: ['./offers.component.css']
})
export class OffersComponent {

  constructor(private router: Router, private titleService: Title, private meta:Meta) { }

  ngOnInit() {
    this.titleService.setTitle('Bien Vivre Bien Manger | Les prestations');
    this.meta.addTag({
      name: 'description', content: 'Découvrez mes prestations sur mesure pour vous accompagner dans une alimentation végétale équilibrée. Consultation individuelle, suivi sur 3 mois ou consultation à domicile, choisissez votre chemin vers un mode de vie sain et durable.'
    });
  }

  redirectToContact() {
    this.router.navigate(['/contact']);
  }
}
