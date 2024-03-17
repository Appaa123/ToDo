import { Component } from '@angular/core';
import { TaskService } from '../services/task.service';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Task } from '../entities/task';
import { FormGroup, FormControl } from '@angular/forms';
import { Color, ScaleType } from '@swimlane/ngx-charts';
import { LegendPosition } from '@swimlane/ngx-charts';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent {

  tasks: any = [];
  chartData :any = [];
  formData: any;
  //Pie chart
  view: [number, number] = [1800, 400];

  // options
  gradient: boolean = true;
  showLegend: boolean = true;
  showLabels: boolean = true;
  isDoughnut: boolean = false;
  public legendPosition: LegendPosition = LegendPosition.Below;
  colorScheme: Color = {
    domain: ['#D20103', '#158FC8', '#26B642','#EEBF17', 
    '#BE0EEE', '#F92D2D','#992DF9','#2DE1F9',
  '#2DF978', '#97F92D', '#F9C92D', '#F9482D', '#F92D2D'],
    group: ScaleType.Ordinal,
    selectable: true,
    name: 'Customer Usage',
};

private unsubscribe$: Subject<void> = new Subject<void>();

  constructor(
    public taskService: TaskService
  ){
    this.formData = new FormGroup({
      name: new FormControl("TaskName"),
      priority: new FormControl(1),
      type: new FormControl("TaskType"),
      percentageCompleted: new FormControl(0),
      description: new FormControl("Task Description"),
      startDate: new FormControl("Task StartDate"),
      endDate: new FormControl("Task EndDate")
   });

   this.getTasks();

  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  onSelect(data: any): void {
    console.log('Item clicked', JSON.parse(JSON.stringify(data)));
  }

  onActivate(data: any): void {
    console.log('Activate', JSON.parse(JSON.stringify(data)));
  }

  onDeactivate(data: any): void {
    console.log('Deactivate', JSON.parse(JSON.stringify(data)));
  }

  getChartData() {
    this.taskService.GetTasks()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((x: any) => {
        this.chartData = x.map((task: Task) => ({
          name: task.name,
          value: task.percentageCompleted.toString()
        }));
      });
  }


  getTasks(){
    this.getChartData();
    return this.taskService.GetTasks().subscribe(x => {
      this.tasks = x;
    })
  }

  // you can use Aync and Promise here to avoid more timeout
  deleteTask(id: any){
    this.taskService.deleteTasks(id);
    setTimeout(() => {
      this.getTasks();
    },1000);
    this.getTasks();
  }

  createTasks(data: any){
    this.taskService.CreateTask(data);
    this.getTasks();
  }

}
