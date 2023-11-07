import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TreeDetailPage } from './tree-detail.page';

const routes: Routes = [
  {
    path: '',
    component: TreeDetailPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TreeDetailPageRoutingModule {}
