import {Component, Input, OnInit} from '@angular/core';
import {Inspection} from '../models/inspection';
import {Repair} from '../models/repair';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-repairs',
  standalone: false,

  templateUrl: './repairs.component.html',
  styleUrl: './repairs.component.css'
})
export class RepairsComponent implements OnInit{
  @Input() repairs: Repair[] = [];
  public displayedColumns: string[] = ['date', 'description', 'cost'];
  public repairForm!: FormGroup;

  constructor(private formBuilder: FormBuilder) {

  }

  ngOnInit(): void {
    this.repairForm = this.formBuilder.group({
      date: ['', Validators.required],
      description: ['', Validators.required],
      cost: ['', [Validators.required, Validators.min(0)]],
    })
  }
}
