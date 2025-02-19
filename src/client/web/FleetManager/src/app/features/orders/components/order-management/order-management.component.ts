import {Component, OnInit} from '@angular/core';
import {OrderManagementService} from './service/order-management.service';
import {DataSource} from '@angular/cdk/collections';
import {Order} from './models/order';
import {Observable, ReplaySubject} from 'rxjs';

@Component({
  selector: 'app-order-management',
  standalone: false,

  templateUrl: './order-management.component.html',
  styleUrl: './order-management.component.css'
})
export class OrderManagementComponent implements OnInit{
  public orderDataSource = new OrderDataSource([]);

  constructor(private orderManagementService: OrderManagementService) {
  }

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders(){
    this.orderManagementService.getOrders().subscribe({
      next: result => {
        this.orderDataSource.setData(result);
      },
    })
  }
}

class OrderDataSource extends DataSource<Order>{
  public columnsToDisplay = ['contractorName', 'originLocation', 'destinationLocation', 'pickupDate', 'deliveryDate'];
  private _dataStream = new ReplaySubject<Order[]>();


  constructor(initialData: Order[]) {
    super();
    this.setData(initialData);
  }

  connect(): Observable<Order[]> {
    return this._dataStream;
  }

  disconnect() {}

  setData(data: Order[]) {
    this._dataStream.next(data);
  }
}


