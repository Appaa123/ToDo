import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Task } from  '../entities/task';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class TaskService {

  baseUrl = "http://localhost:5089/";

  constructor(private http: HttpClient) { }

    // Http Headers
    httpOptions = {
      headers: new HttpHeaders({
        // 'Content-Type': 'multipart/form-data',
        // 'Accept': '*/*',
        // 'Access-Control-Allow-Methods': 'GET, POST, OPTIONS, PUT, PATCH, DELETE'
      })
    };

    //Post
    CreateTask(data: any) {
      let formData: FormData = new FormData();
      formData.append("name", data.name);
      formData.append("priority", data.priority);
      formData.append("type", data.type);
      formData.append("percentageCompleted", data.percentageCompleted);
      formData.append("description", data.description);
      formData.append("startDate", data.startDate);
      formData.append("endDate", data.endDate);
      this.http.post<Task>(
        this.baseUrl + 'api/tasks',
        formData
      ).subscribe(
        error=> console.log(error)
      );
    }


    //Get

    GetTasks(): Observable<Task> {
      return this.http.get<Task>(this.baseUrl + 'api/tasks')
      .pipe(retry(1), catchError(this.errorHandl));
    }

    deleteTasks(id: any) {
      this.http.delete<Task>(this.baseUrl + 'api/tasks/' + id, id)
      .subscribe(error => console.log(error));
    }

     // Error handling
  errorHandl(error: { error: { message: string; }; status: any; message: any; }) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(() => {
      return errorMessage;
    });


  }
}
