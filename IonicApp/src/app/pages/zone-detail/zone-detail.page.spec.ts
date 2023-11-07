import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { IonCard, IonicModule } from '@ionic/angular';

import { ZoneDetailPage } from './zone-detail.page';

describe('ZoneDetailPage', () => {
  let component: ZoneDetailPage;
  let fixture: ComponentFixture<ZoneDetailPage>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ZoneDetailPage ],
      imports: [
        IonicModule.forRoot(),
        RouterTestingModule,
        HttpClientTestingModule
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ZoneDetailPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should get an ion-card with details of a zone', () => {
    const arr = [
      {
        id: 1,
        name: 'Zone 1',
        description: 'The first zone',
      }
    ];
    component.zone = arr;
    let el = fixture.debugElement.query(By.directive(IonCard));
    expect(el).toBeDefined();
  });
});
