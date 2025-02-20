import {Component, OnInit} from '@angular/core';
import {OrderManagementFilterService} from './service/order-management-filter.service';
import {FormBuilder, FormControl, FormGroup} from '@angular/forms';
import {map, Observable, startWith} from 'rxjs';
import {LocationInfo} from '../../../../locations/models/location-info';
import {ContractorInfo} from '../../../../contractors/models/contractor-info';
import {OrderManagementFilter} from '../models/order-management-filter';

@Component({
  selector: 'app-order-management-filter',
  standalone: false,

  templateUrl: './order-management-filter.component.html',
  styleUrl: './order-management-filter.component.css'
})
export class OrderManagementFilterComponent implements OnInit {

  public filterForm: FormGroup;
  public originFormControl: FormControl = new FormControl('');
  public filteredOriginLocations: Observable<LocationInfo[]> = new Observable<LocationInfo[]>();

  public destinationFormControl: FormControl = new FormControl('');
  public filteredDestinationLocations: Observable<LocationInfo[]> = new Observable<LocationInfo[]>();

  public contractorFormControl: FormControl = new FormControl('');
  public filteredContractors: Observable<ContractorInfo[]> = new Observable<ContractorInfo[]>();

  private locationsInfo: LocationInfo[] = [];
  private contractorInfo: ContractorInfo[] = [];


  constructor(private fb: FormBuilder, private orderManagementFilterService: OrderManagementFilterService) {
    this.filterForm = this.fb.group({
      origin: this.originFormControl,
      destination: this.destinationFormControl,
      contractor: this.contractorFormControl,
      pickupDateFrom: [''],
      pickupDateTo: [''],
      deliveryDateFrom: [''],
      deliveryDateTo: ['']
    });
  }

  ngOnInit(): void {
    this.orderManagementFilterService.getOrderManagementFilter().subscribe({
      next: result => {
        this.prepareControls(result)
      }
    })
  }

  private prepareControls(orderManagementFilter: OrderManagementFilter) {
    this.locationsInfo = orderManagementFilter.locationsInfo;
    this.contractorInfo = orderManagementFilter.contractorsInfo;

    this.filteredOriginLocations = this.originFormControl.valueChanges.pipe(
      startWith(''),
      map(value => this.locationFilter(value || '')),
    )

    this.filteredDestinationLocations = this.destinationFormControl.valueChanges.pipe(
      startWith(''),
      map(value => this.locationFilter(value || '')),
    )

    this.filteredContractors = this.contractorFormControl.valueChanges.pipe(
      startWith(''),
      map(value => this.contractorFilter(value || '')),
    )
  }

  public filter(){
    console.log(this.filterForm.value);
  }

  public displayLocation(locationInfo: LocationInfo): string {
    return locationInfo.name;
  }

  private locationFilter(value: string): LocationInfo[] {
    const filterValue = value.toLowerCase();
    return this.locationsInfo.filter(location => location.name.toLowerCase().includes(filterValue));
  }

  private contractorFilter(value: string): ContractorInfo[] {
    const filterValue = value.toLowerCase();
    return this.contractorInfo.filter(contractor => contractor.name.toLowerCase().includes(filterValue));
  }

  public displayContractor(contractorInfo: ContractorInfo): string {
    return contractorInfo.name;
  }
}


