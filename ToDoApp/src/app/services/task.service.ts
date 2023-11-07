import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Task } from  '../entities/task';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class TaskService {

  baseUrl = "http://localhost:5089/"
  constructor(private http: HttpClient) { }

    // Http Headers
    httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };

    //Post
    CreateTask(data: any): Observable<Task> {
      return this.http.post<Task>(
        this.baseUrl + 'api/tasks',
        JSON.stringify(data),
        this.httpOptions
      )
      .pipe(retry(1), catchError(this.errorHandl))
    }


    //Get

    GetTasks(): Observable<Task> {
      return this.http.get<Task>(this.baseUrl + 'api/tasks')
      .pipe(retry(1), catchError(this.errorHandl));
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
