import { Component, OnInit } from '@angular/core';
import { FormComponent } from '../../components/form/form.component';
import { TaskService } from '../../services/task.service';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskInformation } from '../../models/Task';
import { CommonModule } from '@angular/common';
import { EditTaskRequest } from '../../models/EditTaskRequest';

@Component({
  selector: 'app-edit',
  imports: [FormComponent, CommonModule],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.css'
})

export class EditComponent implements OnInit {

  btnAction = 'Edit';
  descPageName = 'Edit Task';

  taskEntity!: TaskInformation;

  constructor(private taskService: TaskService, private Router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    const id = String(this.route.snapshot.paramMap.get('id'));

    this.taskService.GetTaskById(id).subscribe(response => {
      console.log(response);
      this.taskEntity = response;
    });
  }

  EditTaskButton(task: EditTaskRequest) {
    this.taskService.EditTask(task).subscribe(response => {
      this.Router.navigate(['/']);
    });

  }
}
