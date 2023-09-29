import { Component } from '@angular/core';
import { Contact } from '../../Models/contact';
import { ContactService } from '../../Services/Contact/contact.service';
import { ViewportScroller } from '@angular/common';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent {
  contact: Contact = new Contact();
  responseMessage: string = "";
  nameError: string = '';
  emailError: string = '';
  phoneError: string = '';
  subjectError: string = '';
  messageError: string = '';

  constructor(private contactService: ContactService, private viewportScroller: ViewportScroller) { }

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
    if (!this.contact.email) {
      this.emailError = 'Le champ email est requis.';
    }
    if (!this.contact.phone) {
      this.phoneError = 'Le champ téléphone est requis.';
    }
    if (!this.contact.subject) {
      this.subjectError = 'Le champ sujet est requis.';
    }
    if (!this.contact.message) {
      this.messageError = 'Le champ message est requis.';
    }

    // Vérifie s'il y a des erreurs avant de soumettre le formulaire
    if (!this.nameError && !this.emailError && !this.phoneError && !this.subjectError && !this.messageError) {
      // Soumets le formulaire
      this.contactService.SendContactForm(this.contact).subscribe(
        (result: string) => {
          this.responseMessage = 'success';
          this.contact = new Contact();
          this.viewportScroller.scrollToPosition([0, 0]);

        },
        (error: string) => {
          this.responseMessage = 'fail';
          this.viewportScroller.scrollToPosition([0, 0]);
        }
      );
    }
  }
}
