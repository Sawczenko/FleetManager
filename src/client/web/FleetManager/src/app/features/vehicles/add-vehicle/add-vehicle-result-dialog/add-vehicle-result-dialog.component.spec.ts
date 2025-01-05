import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVehicleResultDialogComponent } from './add-vehicle-result-dialog.component';

describe('AddVehicleResultDialogComponent', () => {
  let component: AddVehicleResultDialogComponent;
  let fixture: ComponentFixture<AddVehicleResultDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddVehicleResultDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddVehicleResultDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
