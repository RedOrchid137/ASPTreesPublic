import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';
import { Router } from '@angular/router';


@Component({
  selector: 'app-splashscreen',
  templateUrl: './splashscreen.page.html',
  styleUrls: ['./splashscreen.page.scss'],
})
export class SplashscreenPage implements OnInit {

  constructor(public navCtrl: NavController, private router: Router) {
    // setTimeout(() => {
    //   this.navCtrl.navigateRoot('home');
    // }, 1500);
   }

  ngOnInit() {
  }

  ionViewDidEnter() {
    setTimeout(() => {
      this.router.navigate(['/home']);
    }, 1000);
  }
}