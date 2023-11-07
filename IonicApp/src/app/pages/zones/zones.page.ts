import { Component, OnInit } from '@angular/core';
import { LoadingController } from '@ionic/angular';
import { IZone } from 'src/app/interfaces/IZone';
import { ApiService } from 'src/app/services/api.service';


@Component({
  selector: 'app-zones',
  templateUrl: './zones.page.html',
  styleUrls: ['./zones.page.scss'],
})
export class ZonesPage implements OnInit {

  data: string = '';
  zones: IZone[];
  currentImg = 0;

  images: string[] = [
    '../../../assets/Zone/plantsite1.png',
    '../../../assets/Zone/plantsite2.png',
    '../../../assets/Zone/plantsite3.png'
  ];

  results?

  constructor(
    private api: ApiService,
    private loadingCtrl: LoadingController,
  ) {

  }

  ngOnInit() {
    this.loadZones();
    setInterval(() => {
      this.currentImg = (this.currentImg + 1) % this.images.length;
    }, 5000);
  }

  async loadZones() {
    const loading = await this.loadingCtrl.create({
      message: 'Loading..',
      spinner: 'bubbles',
    });
    await loading.present();
    await this.api.getAllZones().subscribe(
      data => {
        loading.dismiss();
        this.zones = data;
        console.log(data);
      },
      (err) => {
        console.log(err);
      }
    );
  }

  handleChange(event) {
    const query = event.target.value.toLowerCase();
    // this.results = this.zones.filter(d => d.toLowerCase().indexOf(query) > -1);
  }

  public get imageUrl() {
    return this.images[this.currentImg];
  }
}
