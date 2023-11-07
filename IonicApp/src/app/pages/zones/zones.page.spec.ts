import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { IonCard, IonicModule, IonItem } from '@ionic/angular';

import { ZonesPage } from './zones.page';

describe('ZonesPage', () => {
  let component: ZonesPage;
  let fixture: ComponentFixture<ZonesPage>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ZonesPage ],
      imports: [
        IonicModule.forRoot(),
        HttpClientTestingModule
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ZonesPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should show an ion-card when no zones are found', () => {
    const el = fixture.debugElement.query(By.directive(IonCard));
    expect(el).toBeDefined();
    expect(el.nativeNode.textContent.trim()).toBe('No Zones Found');
  });

  // it('should get an array with objects', () => {
  //   const arr = [
  //     {
  //       id: 1,
  //       name: 'Zone 1',
  //       description: 'The first zone'
  //     }
  //   ];
  //   let el = fixture.debugElement.query(By.directive(IonCard));
  //   expect(el).toBeDefined();
  //   expect(el.nativeNode.textContent.trim()).toBe('No Zones Found');
  //   component.zones = arr;

  //   fixture.detectChanges();
  //   el = fixture.debugElement.query(By.directive(IonCard));
  //   expect(el).toBeNull();

  //   const zones = fixture.debugElement.queryAll(By.directive(IonItem));
  //   expect(zones.length).toBe(arr.length);
  // });
});
