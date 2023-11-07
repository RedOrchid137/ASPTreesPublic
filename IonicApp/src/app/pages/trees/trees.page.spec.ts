import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { IonCard, IonicModule, IonItem } from '@ionic/angular';

import { TreesPage } from './trees.page';

describe('TreesPage', () => {
  let component: TreesPage;
  let fixture: ComponentFixture<TreesPage>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ TreesPage ],
      imports: [
        IonicModule.forRoot(),
        HttpClientTestingModule
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(TreesPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  afterEach(() => {
    localStorage.removeItem('data');
    component = null;
  })

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should show an ion-card when no trees are found', () => {
    const el = fixture.debugElement.query(By.directive(IonCard));
    expect(el).toBeDefined();
    expect(el.nativeNode.textContent.trim()).toBe('No Trees Found');
  });

  it('should get an array with objects', () => {
    const arr = [
      {
        id: 1,
        name: 'Mangrove Tree',
        description: 'Originated from swampy area',
        picturePath: null
      }
    ];
    let el = fixture.debugElement.query(By.directive(IonCard));
    expect(el).toBeDefined();
    expect(el.nativeNode.textContent.trim()).toBe('No Trees Found');
    component.trees = arr;

    fixture.detectChanges();
    el = fixture.debugElement.query(By.directive(IonCard));
    expect(el).toBeNull();

    const trees = fixture.debugElement.queryAll(By.directive(IonItem));
    expect(trees.length).toBe(arr.length);
  });
});
