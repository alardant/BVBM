import { Component, OnInit } from '@angular/core';
import { Contact } from '../../Models/contact';
import { ContactService } from '../../Services/Contact/contact.service';
import { ViewportScroller } from '@angular/common';
import { Meta, Title } from '@angular/platform-browser';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {
  contact: Contact = new Contact();
  responseMessage: string = "";
  nameError: string = '';
  emailError: string = '';
  emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
  phoneError: string = '';
  phoneRegex = /^[0-9]+$/;
  subjectError: string = '';
  messageError: string = '';
  captchaError: string = '';
  isCaptchaResolved: boolean = false;

  constructor(private contactService: ContactService, private viewportScroller: ViewportScroller, private titleService: Title, private meta: Meta) { }

  ngOnInit() {
    this.titleService.setTitle('Bien Vivre Bien Manger | Contactez-moi');
    this.meta.addTag({ name: 'description', content: 'Contactez-moi pour toutes questions ou demandes.' });
  }

  sendContactForm() {
    // Réinitialise les messages d'erreur avant chaque soumission
    this.nameError = '';
    this.emailError = '';
    this.phoneError = '';
    this.subjectError = '';
    this.messageError = '';

    // Valide des champs et affichage des messages d'erreur si nécessaire
    if (!this.contact.name) {
      this.nameError = 'Le champ Nom est requis.';
    }
    if (!this.contact.email || !this.emailRegex.test(this.contact.email)) {
      this.emailError = 'Le champ email est requis et l\'adresse email doit être valide.';
    }
    if (!this.contact.phone || !this.phoneRegex.test(this.contact.phone.toString())) {
      this.phoneError = 'Le champ téléphone est requis et doit ne doit comporter que des chiffres.';
    }
    if (!this.contact.subject) {
      this.subjectError = 'Le champ sujet est requis.';
    }
    if (!this.contact.message) {
      this.messageError = 'Le champ message est requis.';
    }
    if (!this.isCaptchaResolved) {
      this.captchaError = 'Le captcha doit être validé.';
    }

    // Vérifie s'il y a des erreurs avant de soumettre le formulaire
    if (!this.nameError && !this.emailError && !this.phoneError && !this.subjectError && !this.messageError && this.isCaptchaResolved == true) {
      // Soumets le formulaire
      
      this.contactService.SendContactForm(this.contact).subscribe(
        (result: string) => {
          this.responseMessage = 'success';
          this.contact = new Contact();
          this.viewportScroller.scrollToPosition([0, 0]);

        },
        (error) => {
          this.responseMessage = 'fail';
          this.viewportScroller.scrollToPosition([0, 0]);
        }
      );
    }
  }

  setCaptacha(resolved: boolean) {
    this.isCaptchaResolved = resolved;
    this.captchaError = '';
  }

}
