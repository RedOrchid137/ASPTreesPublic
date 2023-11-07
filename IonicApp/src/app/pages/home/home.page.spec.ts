import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { AuthModule } from '@auth0/auth0-angular';
import { IonicModule } from '@ionic/angular';
import { Storage } from '@ionic/storage-angular';

import { HomePage } from './home.page';

describe('HomePage', () => {
  let component: HomePage;
  let fixture: ComponentFixture<HomePage>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ HomePage ],
      imports: [
        IonicModule.forRoot(),
        AuthModule.forRoot({
          domain: 'dev-iv2wvcfy528etoco.us.auth0.com',
          clientId: 'VY8UlTPjYd85CPvG3okoRn0jdcLH0eSi',
        }),
        HttpClientTestingModule
      ],
      providers: [Storage]
    }).compileComponents();

    fixture = TestBed.createComponent(HomePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
