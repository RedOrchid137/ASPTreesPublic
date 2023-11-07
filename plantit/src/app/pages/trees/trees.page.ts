import { Component, OnInit } from '@angular/core';
import { LoadingController } from '@ionic/angular';
import { ApiService } from '../../services/api.service';
import { ITreeSpecies } from 'src/app/interfaces/ITreeSpecies';

@Component({
  selector: 'app-trees',
  templateUrl: './trees.page.html',
  styleUrls: ['./trees.page.scss'],
})
export class TreesPage implements OnInit {

  data: string = '';
  trees: ITreeSpecies[];

  constructor(private api: ApiService, private loadingCtrl: LoadingController) {}

  ngOnInit() {
    this.loadTrees();
  }

  async loadTrees() {
    const loading = await this.loadingCtrl.create({
      message: 'Loading..',
      spinner: 'bubbles',
    });
    await loading.present();
    await this.api.getAllTrees().subscribe(
      data => {
        loading.dismiss();
        this.trees = data;
        console.log(data);
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
