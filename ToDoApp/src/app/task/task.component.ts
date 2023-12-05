import { Component } from '@angular/core';
import { TaskService } from '../services/task.service';
import { Observable } from 'rxjs';
import { Task } from '../entities/task';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent {

  tasks: any = [];
  formData: any;
  constructor(
    public taskService: TaskService
  ){ }

  ngOnInit() {
    this.formData = new FormGroup({
       name: new FormControl("TaskName"),
       priority: new FormControl(1),
       type: new FormControl("TaskType"),
       description: new FormControl("Task Description"),
       startDate: new FormControl("Task StartDate"),
       endDate: new FormControl("Task EndDate")
    });
 }

  getTasks(){
    return this.taskService.GetTasks().subscribe(x => {
      this.tasks = x;
    })
  }

  createTasks(data: any){
    this.taskService.CreateTask(data);
    this.getTasks();
  }

}
