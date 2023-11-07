import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { IonDatetimeButton } from '@ionic/angular';
import { Observable } from 'rxjs';
import { concatMap, map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ITask, Status } from '../interfaces/Itask';
import { ITreeSpecies } from '../interfaces/ITreeSpecies';
import { IZone } from '../interfaces/IZone';
@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(public auth: AuthService, private http: HttpClient) {

  }

  getAllTrees(): Observable<ITreeSpecies[]> {
    return this.http.get<ITreeSpecies[]>(`${environment.baseUrl}/TreeSpecies`);
  }

  getTreeDetails(id: string): Observable<ITreeSpecies> {
    return this.http.get<ITreeSpecies>(`${environment.baseUrl}/TreeSpecies/${id}`);
  }
  
  //getAllTasks(): Observable<ITask[]>
  // {
  //   return this.http.get<ITask[]>(`${environment.baseUrl}/EmployeeTask`);
  // }
  // DeleteTask(id: string){
  //   console.log(id);
  //   return this.http.delete(`${environment.baseUrl}/EmployeeTask/${id}`, {
  //     headers: new HttpHeaders({
  //       "Content-Type": "application/json"
  //     })
  //   });
  // }
  // postDoneTask(task: ITask, Done: boolean){
  //   console.log(task);
  //   return this.http.post(environment.baseUrl, Done, {
  //     headers: new HttpHeaders({
  //       "Content-Type": "application/json"
  //     })
  //   });
  // }
  getCurUserTasksToday(page = 1): Observable<ITask[]>
  {
    return this.http.get<ITask[]>(`${environment.baseUrl}/EmployeeTask/UserTasksToday`);
  }

  getCurUserTasksSchedule(page = 1): Observable<ITask[]>
  {
    return this.http.get<ITask[]>(`${environment.baseUrl}/EmployeeTask/UserTasksSchedule`);
  }

  getTaskDetails(id: string)
  {
    return this.http.get(`${environment.baseUrl}/EmployeeTask/${id}`);
  }

  postTaskStart(task:ITask) {
    console.log("starting task")
    task.status=Status.In_Progress
    task.actualStart = new Date()
    
    return this.http.put<ITask>(`${environment.baseUrl}/EmployeeTask/Start`,task);
  }

  postTaskStop(task:ITask) {
    console.log("stopping task")
    task.status=Status.Done
    task.actualStop = new Date()
    return this.http.put<ITask>(`${environment.baseUrl}/EmployeeTask/Stop`,task);
  }

  async postLoginAPI(){
    await this.http.post<any>(`${environment.baseUrl}/Account/Login`,{}).toPromise().then((s)=>{
      console.log(s)
    },(e)=>{
      console.log(e)
    });
  }

  getAllZones(): Observable<IZone[]> {
    return this.http.get<IZone[]>(`${environment.baseUrl}/Zone`);
  }

  getZoneDetails(id: string): Observable<IZone> {
    return this.http.get<IZone>(`${environment.baseUrl}/Zone/${id}`);
  }
}
