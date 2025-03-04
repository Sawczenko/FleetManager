import {Component, Input, OnInit} from '@angular/core';
import {MapDirectionsService} from '@angular/google-maps';
import {Order} from '../models/order';
import {LocationInfo} from '../../../../locations/models/location-info';
import {GoogleMapsService} from '../../../../../core/services/maps/google-maps.service';
import {map, Observable} from 'rxjs';
import LatLng = google.maps.LatLng;

@Component({
  selector: 'app-order-details',
  standalone: false,
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.css'
})
export class OrderDetailsComponent implements OnInit {

  @Input({required: true}) order!: Order;

  private originLocation!: LocationInfo;
  private destinationLocation!: LocationInfo;

  public center: google.maps.LatLngLiteral = {lat: 40.73061, lng: -73.935242};
  public zoom = 12;
  public markers: google.maps.LatLngLiteral[] = [];
  public directionsResults$: Observable<google.maps.DirectionsResult|undefined> = new Observable<google.maps.DirectionsResult|undefined>();


  constructor(private directionsService: MapDirectionsService) {
  }

  ngOnInit(): void {
    const origin = new google.maps.LatLng(this.order.originLocation.latitude, this.order.originLocation.longitude);
    const destination = new google.maps.LatLng(this.order.destinationLocation.latitude, this.order.destinationLocation.longitude);

    this.originLocation = this.order.originLocation;
    this.destinationLocation = this.order.destinationLocation;

    this.markers = [
      {lat: origin.lat(), lng: origin.lng() },
      {lat: destination.lat(), lng: destination.lng()},
    ]

    this.center = {
      lat: (origin.lat() + destination.lat()) / 2,
      lng: (origin.lng() + destination.lng()) / 2,
    };

    const directionsRequest: google.maps.DirectionsRequest = {
      origin: origin,
      destination: destination,
      travelMode: google.maps.TravelMode.DRIVING
    };

    this.directionsResults$ = this.directionsService.route(directionsRequest).pipe(map(response => {
      console.log(response);
      return response.result;
    }));
  }
}
