import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { IonCard, IonicModule, IonItem } from '@ionic/angular';
import { Storage } from '@ionic/storage-angular';

import { TasksPage } from './tasks.page';

describe('TasksPage', () => {
  let component: TasksPage;
  let fixture: ComponentFixture<TasksPage>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ TasksPage ],
      imports: [
        IonicModule.forRoot(),
        HttpClientTestingModule
      ],
      providers: [Storage]
    }).compileComponents();

    fixture = TestBed.createComponent(TasksPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should show an ion-card when no tasks are found', () => {
    const el = fixture.debugElement.query(By.directive(IonCard));
    expect(el).toBeDefined();
    expect(el.nativeNode.textContent.trim()).toBe('No Tasks Found');
  });

  // it('should get an array with objects', () => {
  //   const arr = [
  //     {
  //       id: 1,
  //       name: 'Task 1',
  //       description: 'Do this',
  //     }
  //   ];
  //   let el = fixture.debugElement.query(By.directive(IonCard));
  //   expect(el).toBeDefined();
  //   expect(el.nativeNode.textContent.trim()).toBe('No Tasks Found');
  //   component.tasks = arr;

  //   fixture.detectChanges();
  //   el = fixture.debugElement.query(By.directive(IonCard));
  //   expect(el).toBeNull();

  //   // const tasks = fixture.debugElement.queryAll(By.directive(IonItem));
  //   // expect(tasks.length).toBe(arr.length);
  // });
});
