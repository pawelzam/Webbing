import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUsagesGroupBySim(from: Date, to: Date): Observable<any> {
    const fromDate = new DatePipe('en-US').transform(from, 'yyyy-MM-dd');
    const toDate = new DatePipe('en-US').transform(to, 'yyyy-MM-dd');
    return this.http.get(`${this.baseUrl}/usages-group-by-sim?fromDate=${fromDate}&toDate=${toDate}`);
  }

  getUsagesGroupByCustomer(from: Date, to: Date): Observable<any> { 
    const fromDate = new DatePipe('en-US').transform(from, 'yyyy-MM-dd');
    const toDate = new DatePipe('en-US').transform(to, 'yyyy-MM-dd');
    return this.http.get(`${this.baseUrl}/usages-group-by-customer?fromDate=${fromDate}&toDate=${toDate}`);
  }
}