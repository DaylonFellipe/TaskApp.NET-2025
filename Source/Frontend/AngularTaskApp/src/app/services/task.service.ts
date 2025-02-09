import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TaskInformation } from '../models/Task';
import { AddTaskRequest } from '../models/AddTaskRequest';
import { EditTaskResponse } from '../models/EditTaskResponse';
import { EditTaskRequest } from '../models/EditTaskRequest';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  ApiUrl = environment.UrlApi;

  constructor(private http: HttpClient) { }

  // GET

  GetAllActiveTasks(): Observable<TaskInformation[]> {
    return this.http.get<TaskInformation[]>(this.ApiUrl + '/AllActive');
  }

  GetTaskById(id: string): Observable<TaskInformation> {
    return this.http.get<TaskInformation>(this.ApiUrl + '/ById?guidId=' + id.toString());
  }

  // POST

  RegisterTask(request: AddTaskRequest): Observable<TaskInformation> {
    return this.http.post<TaskInformation>(this.ApiUrl, request);
  }

  // PUT

  EditTask(request: EditTaskRequest): Observable<EditTaskResponse> {
    return this.http.put<EditTaskResponse>(this.ApiUrl + '/NameandDescription', request);
  }

  // DELETE

  SoftDeleteTask(id: string) {
    return this.http.delete<TaskInformation>(this.ApiUrl + '/SoftDeleteOrFinish?guidId=' + id.toString());
  }
}
