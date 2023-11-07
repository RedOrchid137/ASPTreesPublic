import { Component} from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-login-button',
  template: `<ion-button (click)="auth.loginWithRedirect()">Login</ion-button>`,
})
export class LoginButtonComponent {
  constructor(public auth: AuthService) {}

  login(){
    this.auth.loginWithRedirect();
  }
}