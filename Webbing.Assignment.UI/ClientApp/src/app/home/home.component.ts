import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  constructor(private readonly http: HttpClient) {
    
  }

  ngOnInit() : void {
    this.http.get('https://localhost:7187/api/health/check')
      .subscribe();
  }
}
