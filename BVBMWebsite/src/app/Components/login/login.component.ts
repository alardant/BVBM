import { Component } from '@angular/core';
import { UserService } from '../../Services/User/user.service';
import { User } from '../../Models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  user:  User = { email: '', password: '' };; // Modèle pour stocker les informations de connexion
  token: string = ''; // Variable pour stocker le jeton d'authentification
  responseMessage: string = "";

  constructor(private router: Router, private userService: UserService) { }

  login() {
    console.log(this.user);
    // Appeler le service UserService pour effectuer la demande de connexion
    this.userService.Login(this.user).subscribe(
      (token: string) => {
        console.log("connexion ok");
        // Gérer la réponse en cas de succès, par exemple, stocker le jeton d'authentification
        this.token = token;
        this.responseMessage = "Prout";
        this.router.navigate(['/']);
        // Vous pouvez stocker le jeton dans un service d'authentification ou utiliser un gestionnaire de jetons comme ngx-cookie-service.
      },
      (error: string) => {
        // Gérer les erreurs, par exemple, afficher un message d'erreur à l'utilisateur
        console.log("fail");
        this.responseMessage = error;
        this.responseMessage = "Échec de l'authentification, veuillez réessayer";
      }
    );
  }

}
