import {Component, OnInit} from '@angular/core';
import {OrderManagementService} from './service/order-management.service';
import {DataSource} from '@angular/cdk/collections';
import {Order} from './models/order';
import {Observable, ReplaySubject} from 'rxjs';
import {OrdersFilter} from './models/orders-filter';
import {animate, state, style, transition, trigger} from '@angular/animations';
import {GoogleMapsService} from '../../../../core/services/maps/google-maps.service';

@Component({
  selector: 'app-order-management',
  standalone: false,
  animations: [
    trigger('detailExpand', [
      state('collapsed,void', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
  templateUrl: './order-management.component.html',
  styleUrl: './order-management.component.css'
})
export class OrderManagementComponent implements OnInit{
  public mapsLoaded = false;
  public orderDataSource = new OrderDataSource([]);
  expandedElement!: Order | null;

  constructor(private orderManagementService: OrderManagementService, private googleMapsService: GoogleMapsService) {

  }

  ngOnInit(): void {
    this.googleMapsService.loadGoogleMaps().then(() => {
      this.mapsLoaded = true;
    }).catch(error => console.log(error));
    this.getOrders();
  }

  getOrders(ordersFilter?: OrdersFilter){
    this.orderManagementService.getOrders(ordersFilter).subscribe({
      next: result => {
        this.orderDataSource.setData(result);
      },
    })
  }

  public filterChanged(ordersFilter: OrdersFilter){
    this.getOrders(ordersFilter);
  }


}

class OrderDataSource extends DataSource<Order>{
  public columnsToDisplay = ['contractorName', 'originLocation', 'destinationLocation', 'pickupDate', 'deliveryDate', 'expand'];
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


