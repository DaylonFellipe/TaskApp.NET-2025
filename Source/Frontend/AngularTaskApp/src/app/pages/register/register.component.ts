import { Component } from '@angular/core';
import { FormComponent } from '../../components/form/form.component';
import { TaskInformation } from '../../models/Task';
import { TaskService } from '../../services/task.service';
import { response } from 'express';
import { AddTaskRequest } from '../../models/AddTaskRequest';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [FormComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  btnAction = 'Register';
  descPageName = 'Register Task';

  constructor(private taskService: TaskService, private router: Router) { }

  AddTaskButton(task: AddTaskRequest) {
    this.taskService.RegisterTask(task).subscribe(response => {
      this.router.navigate(['/']);
    }
    );
  }

}
