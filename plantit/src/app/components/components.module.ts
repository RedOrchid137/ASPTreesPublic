import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginButtonComponent } from './login-button/login-button.component';
import { LogoutButtonComponent } from './logout-button/logout-button.component';



@NgModule({
  declarations: [LoginButtonComponent, LogoutButtonComponent],
  exports:[LoginButtonComponent, LogoutButtonComponent],
  imports: [
    CommonModule
  ]
})
export class ComponentsModule { }
