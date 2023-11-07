import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

// Import the HTTP interceptor from the Auth0 Angular SDK
import { AuthHttpInterceptor } from '@auth0/auth0-angular';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { SharedComponentsModule } from './components/shared-components.module';
import { AuthModule } from '@auth0/auth0-angular';
import config from '../../capacitor.config';
import auth0 from "./auth0settings.json";
import { IonicStorageModule } from '@ionic/storage-angular';
import { DatePipe } from '@angular/common';
const redirectUri = `${config.appId}://YOUR_DOMAIN/capacitor/${config.appId}/callback`;
@NgModule({
  declarations: [AppComponent],
  entryComponents: [],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    SharedComponentsModule,
    DatePipe,
    AuthModule.forRoot({
  // The domain and clientId were configured in the previous chapter
  domain: auth0.auth0creds.domain,
  clientId: auth0.auth0creds.client_id,
  //redirectUri: redirectUri,
  redirectUri: window.location.origin,
  useRefreshTokens: true,
  cacheLocation: 'localstorage',
  // Specify configuration for the interceptor              
  httpInterceptor: {
    allowedList: [
      {
        uri: auth0.asp_api_creds.audience+'/api/*',
        tokenOptions: {
          // The attached token should target this audience
          audience: auth0.asp_api_creds.audience,
          scope:"openid profile email offline_access"
        }
      }
    ]
  }
    }),
    IonicStorageModule.forRoot()
  ],
  providers: [{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy},{ provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true }],
  bootstrap: [AppComponent],
})
export class AppModule {}
