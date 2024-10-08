import { Component, Inject, OnInit } from '@angular/core';
import { ApiService } from '../../shared/services/api-service';
import { Usage } from '../../shared/models/usage'; // Ensure the import path is correct
import { CustomerInfo } from '../../shared/models/customeInfo';
@Component({
  styleUrls: ['./usage-data.component.css'], // Check if the stylesheet exists
  selector: 'app-usage-data',
  templateUrl: './usage-data.component.html' // Verify the template path
})
export class UsageDataComponent implements OnInit { // Ensure the class implements OnInit correctly
  public usage: Usage[] = [];

  constructor(private readonly apiService: ApiService) {}

  ngOnInit(): void {
    this.getUsagesGroupBySim();
    this.getUsagesGroupByCustomer();
  }

  getUsagesGroupBySim() {
    this.apiService.getUsagesGroupBySim(new Date(2024, 10, 8), new Date(2024, 10, 10)).subscribe((data) => {
      this.usage.push({
        lp: 1,
        count: data.count,        
        title: 'Sims count',
        subtitle: 'Number of all sims',
      });

      this.usage.push({
        lp: 2,
        count: data.quota,        
        title: 'Total usage',
        subtitle: 'Total usage of all sims',
        unit: 'MB'
      });
    });
  }

  getUsagesGroupByCustomer() {
    this.apiService.getUsagesGroupByCustomer(new Date(2024, 9, 8), new Date(2024, 9, 10)).subscribe((data) => {
      this.usage.push({
        count: "2",        
        title: 'Top 2 customers',
        customerInfos: data.topCustomers
      });
    });
  }

  refreshData() {
    this.usage = [];
    this.getUsagesGroupBySim();
    this.getUsagesGroupByCustomer();
  }
}
