import {Component, OnInit} from '@angular/core';
import {OrderManagementFilterService} from './service/order-management-filter.service';

@Component({
  selector: 'app-order-management-filter',
  standalone: false,

  templateUrl: './order-management-filter.component.html',
  styleUrl: './order-management-filter.component.css'
})
export class OrderManagementFilterComponent implements OnInit {
  constructor(private orderManagementFilterService: OrderManagementFilterService) {
  }

    ngOnInit(): void {
        this.orderManagementFilterService.getOrderManagementFilter().subscribe({
          next: result => console.log(result)
        })
    }


}


