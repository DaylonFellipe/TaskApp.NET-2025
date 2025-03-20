import { Component, OnInit } from '@angular/core';
import { TaskInformation } from '../../models/Task';
import { TaskService } from '../../services/task.service';
import { RouterModule } from '@angular/router';
import { CommonModule, DatePipe, SlicePipe } from '@angular/common';
import { CheckboxControlValueAccessor } from '@angular/forms';
import { TaskStatusEnum } from '../../services/Enum/TaskStatusEnum';
import { resourceUsage } from 'process';

@Component({
  selector: 'app-management',
  imports: [RouterModule, DatePipe, SlicePipe, CommonModule],
  templateUrl: './management.component.html',
  styleUrl: './management.component.css'
})
export class ManagementComponent implements OnInit {
  taskList: any[] = [];
  taskAll: TaskInformation[] = [];
  taskStatusEnum = TaskStatusEnum;

  constructor(private task: TaskService) { }

  ngOnInit(): void {
    this.task.GetAllTasks().subscribe((Task) => {
      this.taskList = Task;
      this.taskAll = Task;
      console.log(Task);
      console.log(this.taskList);
    });
  }

  search(event: Event) {
    const target = event.target as HTMLInputElement;
    const value = target.value.toLowerCase();

    this.taskList = this.taskAll.filter(task => {
      return task.name.toLowerCase().includes(value) || task.id.toString().includes(value) || task.description.toLowerCase().includes(value);
    })
  }

  GetTaskbyStatus(taskStatusEnum?: TaskStatusEnum) {
    this.task.GetAllTasks(taskStatusEnum).subscribe(Task => {
      this.taskList = Task;
      console.log(Task);
    });
  }

  ChangeStatusActive(id: string) {
    this.task.ChangeStatusActive(id).subscribe(Task => {
      console.log(Task);
      window.location.reload();
    })
  }

  DeleteTask(id: string) {
    this.task.DeleteTask(id).subscribe(Task => {
      console.log(Task);
      window.location.reload();
    })

  }
}
