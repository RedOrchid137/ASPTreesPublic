import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { TestBed, waitForAsync } from '@angular/core/testing';

import { RouterTestingModule } from '@angular/router/testing';
import { AuthModule } from '@auth0/auth0-angular';

import { AppComponent } from './app.component';

describe('AppComponent', () => {


  beforeEach(waitForAsync(() => {

    TestBed.configureTestingModule({
      declarations: [AppComponent],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
      imports: [
        RouterTestingModule.withRoutes([]),
        AuthModule.forRoot({
          domain: 'dev-iv2wvcfy528etoco.us.auth0.com',
          clientId: 'VY8UlTPjYd85CPvG3okoRn0jdcLH0eSi',
        }),
      ],
    }).compileComponents();
  }));

  it('should create the app', waitForAsync(() => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));

  it('should have menu labels', waitForAsync(() => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const app = fixture.nativeElement;
    const menuItems = app.querySelectorAll('ion-label');
    expect(menuItems.length).toEqual(5);
    expect(menuItems[0].textContent).toContain('Home');
    expect(menuItems[1].textContent).toContain('Tasks');
    expect(menuItems[2].textContent).toContain('Zones');
    expect(menuItems[3].textContent).toContain('Trees');
    expect(menuItems[4].textContent).toContain('Scan');
  }));

  it('should have urls', waitForAsync(() => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const app = fixture.nativeElement;
    const menuItems = app.querySelectorAll('ion-item');
    expect(menuItems.length).toEqual(5);
    expect(menuItems[0].getAttribute('ng-reflect-router-link')).toEqual('/home');
    expect(menuItems[1].getAttribute('ng-reflect-router-link')).toEqual('/tasks');
    expect(menuItems[2].getAttribute('ng-reflect-router-link')).toEqual('/zones');
    expect(menuItems[3].getAttribute('ng-reflect-router-link')).toEqual('/trees');
  }));

});
