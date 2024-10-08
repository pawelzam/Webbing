import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-usage-data',
  templateUrl: './usage-data.component.html'
})
export class UsageDataComponent {
  public usage: any[] = [];

}
