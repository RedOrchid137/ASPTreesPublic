import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import {ApiService } from '../../services/api.service';
import { IonDatetime, LoadingController, ModalController } from '@ionic/angular';
import { TodoService } from 'src/app/services/todo.service';
import { UpdateTaskPage } from '../update-task/update-task.page';
import { ITask, Status } from 'src/app/interfaces/Itask';
import { Priority } from '../../interfaces/ITask';
import { DatePipe } from '@angular/common';

// export class Tasks{
//   WorkScheduleId: string;
//   name: string;
//   description: string;
//   WorkDay: IonDatetime
//   //workschedule: WorkSchedule;
// }
@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.page.html',
  styleUrls: ['./tasks.page.scss'],
})


export class TasksPage implements OnInit {
  

  state:Status = Status.Todo
  scheduleMode = "outline";
  dayMode = "solid";
  taskSource = true;
  
  
  currentPage = 1;
  tasks: ITask[] = [];
  filterTasks:ITask[] = [];
  activeTasks = {};
  todoList = [];

  today: number = Date.now();
  _data: [""];
  results?

  constructor(
    private httpClient: HttpClient, 
    private api: ApiService, 
    private loadingCtrl: LoadingController, 
    public modalCtrl: ModalController, 
    public todoService: TodoService,
    public datepipe: DatePipe) 
    {


    // this.getTasks().subscribe(res => {
    //   console.log(res);
    //   this.Tasks = res;
    // });  
    // this.getallTasks();
  }


  

  checked : boolean = true;
  complete(e): void {
    var isChecked = e.currentTarget.checked;
    console.log(this.checked);
    console.log(this.checked);
  }

  async ngOnInit() {
    await this.loadTasks();
    this.getTasksState()
  }

  async loadTasks() {
    const loading = await this.loadingCtrl.create({
      message: 'Loading..',
      spinner: 'bubbles',
    });
    //await loading.present();

    if(this.taskSource){
      await this.api.getCurUserTasksToday().toPromise().then(
        data => {
          loading.dismiss();
          this.tasks = data;
          console.log(data);
          this.results = [...data];
        },
        (err) => {
          console.log(err);
        }
      );
    }
    else{
      await this.api.getCurUserTasksSchedule().toPromise().then(
        data => {
          loading.dismiss();
          this.tasks = data;
          console.log(data);
          this.results = [...data];
        },
        (err) => {
          console.log(err);
        }
      );
    }
  }

  handleChange(event) {
    const query = event.target.value.toLowerCase();
    this.results = this._data.filter(d => d.toLowerCase().indexOf(query) > -1);
  }

  delete(key){
    // this.todoList.splice(index, 1)
    this.todoService.deleteTask(key);
  }

  markAsDone(id){
    console.log("Item" + {id} +"Completed")
    this.delete(id);
  }

  async update(selectedTask){
    const modal = await this.modalCtrl.create({
      component: UpdateTaskPage,
      componentProps: {task: selectedTask}
    });
    return await modal.present();
  }

  // async activateTask(_task: ITask){
  //   let uid = _task.name; //+ _task.workday;
  //   console.log(uid);
  //   this.todoService.addTask(uid, _task);
  // }

  async startTask(task:ITask){
    let uid = task.name; //+ _task.workday;
    console.log(uid);
    await this.api.postTaskStart(task).toPromise().then(
      data => {
        console.log(data);
      },
      (err) => {
        console.log(err);
      }
    );
    this.todoService.addTask(uid, task);
    this.ngOnInit()
  }
  async stopTask(task:ITask){
    await this.api.postTaskStop(task).toPromise().then(
      data => {
        console.log(data);
        this.results = data
      },
      (err) => {
        console.log(err);
      }
    );
    this.ngOnInit()
  }

  updateState(newState:Status){
    if(this.state!=newState){
      this.state = newState
    }
    this.getTasksState()
  }

  getTasksState(){
    if(this.state==0){
      this.filterTasks = this.tasks.filter(e=>e.status==Status.Todo)
    }
        
    else if(this.state==1){
      console.log("state 1")
      this.filterTasks = this.tasks.filter(e=>e.status==Status.In_Progress)
    }
        
    else{
      console.log("state 2")
      this.filterTasks = this.tasks.filter(e=>e.status==Status.Done)
      
    }
    console.log("tasks")
    console.log(this.tasks)
    console.log("filtered")
    console.log(this.filterTasks)
  }
  ToggleMode(){
    if(this.scheduleMode=="solid"){
      this.scheduleMode = "outline";
      this.dayMode = "solid";
    }
    else{
      
      this.scheduleMode = "solid";
      this.dayMode = "outline";
    }
    this.taskSource = !this.taskSource
    this.ngOnInit()
  }
  getPriority(priority:Priority){
    return Priority[priority]
  }

  getFormattedDateHours(input:Date){
    return this.datepipe.transform(input, 'HH:mm')
  }

  getFormattedDate(input:Date){
    return this.datepipe.transform(input, 'dd/MM/yyyy')
  }

}
