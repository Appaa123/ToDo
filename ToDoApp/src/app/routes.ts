import { Routes } from '@angular/router';
import { TaskComponent } from './task/task.component';
import { AppComponent } from './app.component';

const routeConfig: Routes = [
    {
        path: '',
        component: AppComponent,
        title: 'Home page'
      },    
    {
      path: 'tasks',
      component: TaskComponent,
      title: 'Tasksdetails'
    }
  ];
  
  export default routeConfig;