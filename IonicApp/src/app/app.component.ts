import { Component, OnInit, NgZone } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { mergeMap } from 'rxjs/operators';
import { Browser } from '@capacitor/browser';
import { App } from '@capacitor/app';
import config from 'capacitor.config';
import { ApiService } from './services/api.service';
import { Router } from '@angular/router';

const callbackUri = `${config.appId}://dev-iv2wvcfy528etoco.us.auth0.com/capacitor/${config.appId}/callback`;

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent implements OnInit {
  constructor(public auth: AuthService, private ngZone: NgZone, private router: Router) {this.router.navigateByUrl('/splashscreen');}
  async ngOnInit(){
    // Use Capacitor's App plugin to subscribe to the `appUrlOpen` event
    App.addListener('appUrlOpen', ({ url }) => {
      this.ngZone.run(() => {
        if (url?.startsWith(callbackUri)) {
          if (
            url.includes('state=') &&
            (url.includes('error=') || url.includes('code='))
          ) {
            this.auth
              .handleRedirectCallback(url)
              .pipe(mergeMap(() => Browser.close()))
              .subscribe();
          } else {
            Browser.close();
          }
        }
      });
    });
  }

  public appPages = [
    { title: 'Home', url: '/home', icon: 'home' },
    { title: 'Tasks', url: '/tasks', icon: 'hammer' },
    { title: 'Zones', url: '/zones', icon: 'map' },
    { title: 'Trees', url: '/trees', icon: 'leaf' },
    { title: 'Scan', url: '/scan', icon: 'qr-code' },
  ];

}
