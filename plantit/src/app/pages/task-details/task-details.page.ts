import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ModalController } from '@ionic/angular';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.page.html',
  styleUrls: ['./task-details.page.scss'],
})
export class TaskDetailsPage implements OnInit {

  task = null;

  constructor(private route: ActivatedRoute, private api: ApiService, public modalCtrl: ModalController) { }

  ngOnInit() { 
    this.getDetails();
  }

  getDetails(){
    const id = this.route.snapshot.paramMap.get('id');
    this.api.getTaskDetails(id).subscribe((res) => {
      this.task = res;
    })
  }
  
  // async dismiss() {
  //   await this.modalCtrl.dismiss(this.task)
  // }
}
