import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { IonCard, IonicModule } from '@ionic/angular';

import { TaskDetailsPage } from './task-details.page';


describe('TaskDetailsPage', () => {
  let component: TaskDetailsPage;
  let fixture: ComponentFixture<TaskDetailsPage>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ TaskDetailsPage ],
      imports: [
        IonicModule.forRoot(),
        RouterTestingModule,
        HttpClientTestingModule],
    }).compileComponents();

    fixture = TestBed.createComponent(TaskDetailsPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should return an ion-card', ()=>{
    let el = fixture.debugElement.query(By.directive(IonCard));
    expect(el).toBeDefined();
  });
  it('should return data in the ion-card', ()=>{
    const arr = [
      {
        id: 1,
        name: 'Magrove Task',
        description: 'This is a mangrove tree',
      }
    ];
    component.task = arr;
    let el = fixture.debugElement.query(By.directive(IonCard));
    expect(el).toBeDefined();
  });
});
