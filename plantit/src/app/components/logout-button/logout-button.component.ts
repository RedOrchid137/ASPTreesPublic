import { Component, Inject } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-logout-button',
  template: `
  <ion-button (click)="logout()">
    Log out
  </ion-button>
`,
styles: [],
})
export class LogoutButtonComponent{
  constructor(@Inject(DOCUMENT) public document: Document, public auth: AuthService) {}
 
  logout(){
    this.auth.logout({ returnTo: "http://localhost:8100" });
    
  }
}