import {Component, OnInit} from '@angular/core';
import {OrderManagementService} from './service/order-management.service';

@Component({
  selector: 'app-order-management',
  standalone: false,

  templateUrl: './order-management.component.html',
  styleUrl: './order-management.component.css'
})
export class OrderManagementComponent implements OnInit{
  constructor(private orderManagementService: OrderManagementService) {
  }

  ngOnInit(): void {
    this.orderManagementService.getOrders().subscribe({
      next: result => console.log(result),
    })
  }


}
