import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-add-new-task',
  templateUrl: './add-new-task.page.html',
  styleUrls: ['./add-new-task.page.scss'],
})
export class AddNewTaskPage implements OnInit {

  categories = []
  categorySelectedCategory

  taskObject = {}
  taskName
  taskDate
  taskPriority
  taskCategory

  
  constructor(public modalCtrl: ModalController, public todoService: TodoService) { }

  ngOnInit() {
    this.categories.push('work')
    this.categories.push('personal')
    this.categories.push('home')
  }

  async AddTask(){
    this.taskObject = (
      {
        itemName: this.taskName, 
        itemDueDate: this.taskDate, 
        itemPriority: this.taskPriority,
        itemCategory: this.taskCategory
      })
    console.log(this.taskObject);
    let uid = this.taskName + this.taskDate;

    if(uid){
      await this.todoService.addTask(uid, this.taskObject)
    }else{
      console.log("Can't save an empty task!")
    }
    this.dismiss()
  }

  selectedCategory(index) {
    this.taskCategory = this.categories[index]
  }

  async dismiss() {
    await this.modalCtrl.dismiss(this.taskObject)
  }
}
