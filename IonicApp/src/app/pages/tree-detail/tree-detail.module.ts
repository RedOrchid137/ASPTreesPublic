import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { TreeDetailPageRoutingModule } from './tree-detail-routing.module';

import { TreeDetailPage } from './tree-detail.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    TreeDetailPageRoutingModule
  ],
  declarations: [TreeDetailPage]
})
export class TreeDetailPageModule {}
