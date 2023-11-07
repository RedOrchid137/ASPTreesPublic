import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PasswdResetPage } from './passwd-reset.page';

const routes: Routes = [
  {
    path: '',
    component: PasswdResetPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PasswdResetPageRoutingModule {}
