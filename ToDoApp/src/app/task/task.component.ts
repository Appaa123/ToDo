import { Component } from '@angular/core';
import { TaskService } from '../services/task.service';
import { Observable } from 'rxjs';
import { Task } from '../entities/task';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent {

  tasks: any = [];
  constructor(
    public taskService: TaskService
  ){ }

  getTasks(){
    return this.taskService.GetTasks().subscribe(x => {
      this.tasks = x;
    })
  }

}
