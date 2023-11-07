import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { IonCard, IonicModule } from '@ionic/angular';

import { TreeDetailPage } from './tree-detail.page';

describe('TreeDetailPage', () => {
  let component: TreeDetailPage;
  let fixture: ComponentFixture<TreeDetailPage>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ TreeDetailPage ],
      imports: [
        IonicModule.forRoot(),
        RouterTestingModule,
        HttpClientTestingModule
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(TreeDetailPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should get an ion-card with details of a tree', () => {
    const arr = [
      {
        id: 1,
        name: 'Mangrove Tree',
        description: 'Originated from swampy area',
        picturePath: null
      }
    ];
    component.tree = arr;
    let el = fixture.debugElement.query(By.directive(IonCard));
    expect(el).toBeDefined();
  });
});
