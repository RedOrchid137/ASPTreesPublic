import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-zone-detail',
  templateUrl: './zone-detail.page.html',
  styleUrls: ['./zone-detail.page.scss'],
})
export class ZoneDetailPage implements OnInit {

  zone = null;

  constructor(
    private route: ActivatedRoute,
    private api: ApiService
  ) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.api.getZoneDetails(id).subscribe((res) => {
      this.zone = res;
    })
  }

}
