import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUsagesGroupBySim(from: Date, to: Date): Observable<any> {
    const fromDate = this.formatDate(from);
    const toDate = this.formatDate(to);
    return this.http.get(`${this.baseUrl}/usages-group-by-sim?fromDate=${fromDate}&toDate=${toDate}`);
  }

  getUsagesGroupByCustomer(from: Date, to: Date): Observable<any> { 
    const fromDate = this.formatDate(from);
    const toDate = this.formatDate(to);
    return this.http.get(`${this.baseUrl}/usages-group-by-customer?fromDate=${fromDate}&toDate=${toDate}`);
  }

  private formatDate(date: Date): string {
    return date.toISOString().split('T')[0];
  }
}