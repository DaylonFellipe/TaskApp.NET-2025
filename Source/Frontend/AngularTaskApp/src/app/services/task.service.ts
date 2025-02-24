import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TaskInformation } from '../models/Task';
import { AddTaskRequest } from '../models/AddTaskRequest';
import { EditTaskResponse } from '../models/EditTaskResponse';
import { EditTaskRequest } from '../models/EditTaskRequest';
import { TaskStatusEnum } from './Enum/TaskStatusEnum';
import { TaskInformationDTO } from '../models/TaskDTO';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  ApiUrl = environment.UrlApi;

  constructor(private http: HttpClient) { }

  // GET

  GetAllTasks(status?: TaskStatusEnum): Observable<TaskInformation[]> {
    if (status == undefined)
      return this.http.get<TaskInformation[]>(this.ApiUrl);
    else {
      return this.http.get<TaskInformation[]>(this.ApiUrl + "?status=" + status);
    }
  }

  GetTaskDTO(): Observable<TaskInformationDTO[]> {
    return this.http.get<TaskInformationDTO[]>(this.ApiUrl + "/DTO");
  }

  GetTaskById(id: string): Observable<TaskInformation> {
    return this.http.get<TaskInformation>(this.ApiUrl + '/' + id.toString());
  }

  // POST

  RegisterTask(request: AddTaskRequest): Observable<TaskInformation> {
    return this.http.post<TaskInformation>(this.ApiUrl, request);
  }

  // PUT

  EditTask(request: EditTaskRequest): Observable<EditTaskResponse> {
    return this.http.put<EditTaskResponse>(this.ApiUrl, request);
  }

  // PATCH

  ChangeStatusActive(id: string): Observable<TaskInformation> {
    return this.http.patch<TaskInformation>(this.ApiUrl + '/ActiveStatus' + id.toString(), null);
  }

  // DELETE

  DeleteTask(id: string) {
    return this.http.delete<TaskInformation>(this.ApiUrl + '/' + id.toString());
  }
}
