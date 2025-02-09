import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TaskInformation } from '../../models/Task';
import { AddTaskRequest } from '../../models/AddTaskRequest';
import { CommonModule } from '@angular/common';
import { EditTaskRequest } from '../../models/EditTaskRequest';

@Component({
  selector: 'app-form',
  imports: [RouterModule, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './form.component.html',
  styleUrl: './form.component.css'
})
export class FormComponent implements OnInit {

  constructor(public routerForm: Router) { }

  @Input() btnAction!: string;
  @Input() descPageName!: string;
  @Input() taskData: TaskInformation | null = null;
  @Output() onSubmitRegister = new EventEmitter<AddTaskRequest>();
  @Output() onSubmitEdit = new EventEmitter<EditTaskRequest>();

  taskForm!: FormGroup;

  ngOnInit(): void {

    console.log(this.taskData)

    this.taskForm = new FormGroup({
      id: new FormControl(this.taskData ? this.taskData.id : null),
      name: new FormControl(this.taskData ? this.taskData.name : null),
      description: new FormControl(this.taskData ? this.taskData.description : null),
      createdOn: new FormControl(this.taskData ? this.taskData.createdOn : null),
      active: new FormControl(this.taskData ? this.taskData.active : null),
    });
  }

  submitRegister() {
    this.onSubmitRegister.emit(this.taskForm.value);
  }
  submitEdit() {
    this.onSubmitEdit.emit(this.taskForm.value);
  }
}