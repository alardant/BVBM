import { Component, OnInit } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';

@Component({
  selector: 'app-about-me',
  templateUrl: './about-me.component.html',
  styleUrls: ['./about-me.component.css']
})
export class AboutMeComponent implements OnInit {

  constructor(private meta: Meta, private titleService: Title) { }

  ngOnInit() {
    // Méta Titre
    const metaTitle = "Bien Vivre Bien Manger | Mon Parcours et la Genèse du projet";
    this.titleService.setTitle(metaTitle);

    // Méta Description
    const metaDescription = "Découvrez mon parcours personnel et les objectifs de Bien Vivre Bien Manger, axés sur une alimentation saine et éthique.";
    this.meta.addTag({ name: 'description', content: metaDescription });
  }
}
