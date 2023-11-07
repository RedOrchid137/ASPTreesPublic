import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-update-task',
  templateUrl: './update-task.page.html',
  styleUrls: ['./update-task.page.scss'],
})
export class UpdateTaskPage implements OnInit {
  @Input() task;
  categories = []
  categorySelectedCategory

  taskObject = {}
  taskName;
  taskDate;
  taskPriority;
  taskCategory;
  constructor(public modalCtlr:ModalController, public todoServive:TodoService) { }

  ngOnInit() {
    this.categories.push('work')
    this.categories.push('personal')

    this.taskName = this.task.value.taskName;
    this.taskDate = this.task.value.taskDate;
    this.taskPriority = this.task.value.taskPriority;
    this.categorySelectedCategory = this.task.value.taskCategory;
  }

  selectedCategory(index) {
    this.taskCategory = this.categories[index]
    console.log(this.categorySelectedCategory);
  }

  async dismiss(){
    await this.modalCtlr.dismiss()
  }

  async update(){
    this.taskObject = ({taskName:this.taskName, taskDate:this.taskDate, taskPriority:this.taskPriority,taskCategory:this.categorySelectedCategory})
    let uid = this.task.key
    await this.todoServive.updateTask(uid,this.taskObject)
    this.dismiss()
  }
}
