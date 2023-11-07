import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { PasswdResetPageRoutingModule } from './passwd-reset-routing.module';

import { PasswdResetPage } from './passwd-reset.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    PasswdResetPageRoutingModule
  ],
  declarations: [PasswdResetPage]
})
export class PasswdResetPageModule {}
