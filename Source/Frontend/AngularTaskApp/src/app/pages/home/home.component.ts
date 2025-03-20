import { Component, OnInit } from '@angular/core';
import { TaskService } from '../../services/task.service';
import { TaskInformation } from '../../models/Task';
import { RouterModule } from '@angular/router';
import { CommonModule, DatePipe, SlicePipe } from '@angular/common';
import { TaskStatusEnum } from '../../services/Enum/TaskStatusEnum';

@Component({
  selector: 'app-home',
  imports: [RouterModule, DatePipe, SlicePipe, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  taskList: any[] = [];
  taskAll: TaskInformation[] = [];

  constructor(private task: TaskService) { }

  ngOnInit(): void {
    this.task.GetAllTasks(TaskStatusEnum.active).subscribe((Task) => {
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
      return task.name.toLowerCase().includes(value) || task.description.toLowerCase().includes(value);
    })
  }

  ChangeActiveStatus(id: string) {
    this.task.ChangeStatusActive(id).subscribe(Task => {
      console.log(Task);
      window.location.reload();
    })
  }

}
