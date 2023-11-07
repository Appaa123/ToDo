import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TaskComponent } from './task/task.component';

@Component({
  selector: 'app-root',  
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ToDoApp';
}
