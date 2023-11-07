import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { TreesPageRoutingModule } from './trees-routing.module';

import { TreesPage } from './trees.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    TreesPageRoutingModule
  ],
  declarations: [TreesPage]
})
export class TreesPageModule {}
