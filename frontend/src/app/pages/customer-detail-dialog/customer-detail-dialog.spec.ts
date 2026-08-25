import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerDetailDialog } from './customer-detail-dialog';

describe('CustomerDetailDialog', () => {
  let component: CustomerDetailDialog;
  let fixture: ComponentFixture<CustomerDetailDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerDetailDialog],
    }).compileComponents();

    fixture = TestBed.createComponent(CustomerDetailDialog);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
