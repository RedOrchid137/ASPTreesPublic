import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { LoginPageRoutingModule } from './login-routing.module';

import { LoginPage } from './login.page';
import { ImportsModule } from '../../../imports-module/imports-module.module';

@NgModule({
  imports: [
    ImportsModule
  ],
  declarations: [LoginPage]
})
export class LoginPageModule {}
