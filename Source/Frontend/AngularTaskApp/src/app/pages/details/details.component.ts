import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { TaskInformation } from '../../models/Task';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-details',
  imports: [RouterModule, DatePipe],
  templateUrl: './details.component.html',
  styleUrl: './details.component.css'
})
export class DetailsComponent implements OnInit {

  task!: TaskInformation;

  constructor(private taskService: TaskService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));

    this.taskService.GetTaskById(id).subscribe(response => {
      this.task = response;
    });
  }

}
