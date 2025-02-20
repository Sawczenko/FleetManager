import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup} from '@angular/forms';
import {RoutesFilter} from './models/routes-filter';

@Component({
  selector: 'app-filter',
  standalone: false,

  templateUrl: './filter.component.html',
  styleUrl: './filter.component.css'
})
export class FilterComponent {
  @Output() filterChanged = new EventEmitter<RoutesFilter>();
  public filterForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.filterForm = this.fb.group({
      userName: [''],
      startLocation: [''],
      endLocation: [''],
      status: [''],
      scheduledStartTime: [''],
      scheduledEndTime: ['']
    });
  }

  onFilterChanged() {
    this.filterChanged.emit(this.getFilter());
  }

  onReset() {
    this.filterForm.reset({
      status: '',
      startLocation: '',
      endLocation: '',
      scheduledStartTime: '',
      scheduledEndTime: '',
      userName: '',
    });

    this.filterChanged.emit(this.getFilter());
  }

  private getFilter(): RoutesFilter {
    const formValue = this.filterForm.value;

    return new RoutesFilter(
      formValue.userName,
      formValue.startLocation,
      formValue.endLocation,
      formValue.status,
      formValue.scheduledStartTime,
      formValue.scheduledEndTime
    )
  }
}
