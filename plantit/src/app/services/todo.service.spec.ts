import { TestBed } from '@angular/core/testing';
import { TodoService } from './todo.service';
import { Storage } from '@ionic/storage-angular';

describe('TodoService', () => {
  let service: TodoService;
  let storage: Storage;

  beforeEach(() => {
    storage = new Storage({});
    service = new TodoService(storage);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should add a task to storage', async () => {
    service.addTask('key', 'value');
    const value = await storage.get('key');
    await expect(value).toBe('value');
  });
  

  it('should delete a task from storage', async () => {
    storage.set('key', 'value');
    service.deleteTask('key');
    const value = await storage.get('key');
    await expect(value).toBeNull();
  });
  
  it('should update a task in storage', async () => {
    storage.set('key', 'value');
    service.updateTask('key', 'newValue');
    const value = await storage.get('key');
    await expect(value).toEqual('newValue');
  });
  
  it('should get all tasks from storage', async () => {
    storage.set('key1', 'value1');
    storage.set('key2', 'value2');
    const tasks = await service.getAllTasks();
    await expect(tasks).toEqual([
      { key: 'value1', value: 'key1' },
      { key: 'value2', value: 'key2' }
    ]);
  });
});