import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ZoneDetailPageRoutingModule } from './zone-detail-routing.module';

import { ZoneDetailPage } from './zone-detail.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ZoneDetailPageRoutingModule
  ],
  declarations: [ZoneDetailPage]
})
export class ZoneDetailPageModule {}
