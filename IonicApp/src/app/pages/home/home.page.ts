import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { switchMap } from 'rxjs/operators';
import { ModalController } from '@ionic/angular';
import { ApiService } from 'src/app/services/api.service';
import { TodoService } from 'src/app/services/todo.service';
import { AddNewTaskPage } from '../add-new-task/add-new-task.page';
import { UpdateTaskPage } from '../update-task/update-task.page';
import { ITask } from 'src/app/interfaces/Itask';
import { interval } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
})
export class HomePage implements OnInit {
  
  user = [{
    name: 'A. Cant',
    email: 'arvo.cant@student.ap.be'
  }]

  todoList = [{
    itemName: 'Check up on trees in zone 1',
    itemDueDate: '12-13-22',
    itemPriority: 'low',
    itemCategory: 'Zone 1'
  },
  {
    itemName: 'Mow the grass in zone 4',
    itemDueDate: '12-6-22',
    itemPriority: 'high',
    itemCategory: 'Zone 2'
  },
  {
    itemName: 'Check the stock in the warehouse',
    itemDueDate: '12-4-22',
    itemPriority: 'middle',
    itemCategory: 'Main'
  },]

  today: number = Date.now();
  data: string = '';
  tasks: ITask[];
  activeTask: [{}];
  completedTasks: [{}];

  percentageCompleted: number = 0;
  treesPlanted = 0;

  constructor(public auth: AuthService, private api: ApiService, public modalCtrl: ModalController, public todoService: TodoService) {
    const intervalObservable = interval(5000);

    // Subscribe to the interval observable and specify a callback function to be executed every time the observable emits a value
    intervalObservable.subscribe(() => {
      // Generate a random number between 1 and 3
      const randomNumber = Math.floor(Math.random() * 11) + 1;

      // Add the random number to the value property
      this.treesPlanted += randomNumber;
    });
   }

  async ngOnInit() {
    this.api.postLoginAPI()
    this.loadTasks();
    console.log("Get all tasks komt nu ---")
    this.getallTasks();
    console.log("En stopt hier ---")
    console.log("Get all tasks komt nu ---")
    this.getallTasks();
    console.log("En stopt hier ---")
  }

  async loadTasks(){

    await this.api.getCurUserTasksToday().subscribe(
      data => {
        this.tasks = data;
        console.log(data);
      },
      (err) => {
        console.log(err);
      }
    );
  }

  async addTask(){
    const modal = await this.modalCtrl.create({
      component: AddNewTaskPage
    })
    
    modal.onDidDismiss().then(newTaskobj =>{
      // console.log(newTaskobj.data);
      // this.todoList.push(newTaskobj.data)
      this.getallTasks();
    })
    return await modal.present()
  }

  getallTasks(){
    // this.todoList = this.todoService.getAllTasks();
    this.activeTask = this.todoService.getAllTasks();
    console.log("test")
    console.log(this.activeTask);
  }

  delete(key){
    // this.todoList.splice(index, 1)
    this.todoService.deleteTask(key);
    this.getallTasks();
  }

  markAsDone(index){
    console.log("Item" + {index} +"Completed");
    this.completedTasks.push(this.activeTask[index]);
    this.delete(index);

    this.percentageCompleted = (this.completedTasks.length / this.activeTask.length) * 100;
  }

  async update(selectedTask){
    const modal = await this.modalCtrl.create({
      component: UpdateTaskPage,
      componentProps: {task: selectedTask}
    });
    return await modal.present();
  }
}
