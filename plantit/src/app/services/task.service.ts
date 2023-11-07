import { Injectable } from '@angular/core';
import { TasksPage } from '../pages/tasks/tasks.page';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private _storage: Storage;

  constructor(private storage: Storage) { 
  this.init();
  }

  async init() {
    const Storage = await this.storage.create();
    this._storage = Storage;
    // await this.storage.create()
  }

  public set(key: string, value: any) {
    this._storage?.set(key, value);
  }
  
}
