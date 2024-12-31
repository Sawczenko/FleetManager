import {Component, Input, OnInit} from '@angular/core';
import {Inspection} from '../models/inspection';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {HomeDashboardService} from '../../../dashboard/home-dashboard/service/home-dashboard.service';

@Component({
  selector: 'app-inspections',
  standalone: false,

  templateUrl: './inspections.component.html',
  styleUrl: './inspections.component.css'
})
export class InspectionsComponent implements OnInit{
  @Input() inspections: Inspection[] = [];
  public displayedColumns: string[] = ['date', 'description', 'cost'];
  public inspectionForm!: FormGroup;

  constructor(private formBuilder: FormBuilder) {

  }

  ngOnInit(): void {
    this.inspectionForm = this.formBuilder.group({
      date: ['', Validators.required],
      description: ['', Validators.required],
      cost: ['', [Validators.required, Validators.min(0)]],
    })
  }
}
