import { Component, inject } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomerDetailDialog } from '../customer-detail-dialog/customer-detail-dialog';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-customers',
  imports: [CommonModule],
  templateUrl: './customers.html',
  styleUrl: './customers.css',
})
export class Customers {

  private modalService: NgbModal = inject(NgbModal);
  httpClient = inject(HttpClient);
  CustomerDetails: any;

  openAddModal(): void {
  const modalRef = this.modalService.open(CustomerDetailDialog);
  modalRef.componentInstance.isUpdate = false;
}

openEditModal(customer: any): void {
  const modalRef = this.modalService.open(CustomerDetailDialog);
  modalRef.componentInstance.isUpdate = true;
  modalRef.componentInstance.CustomerDetails = {
    CustomerId: customer.customerId,
    CustomerName: customer.customerName,
    CustomerEmail: customer.customerEmail,
    CustomerPhone: customer.customerPhone,
    RegistrationDate: customer.registrationDate
  };
}
  ngOnInit() {
    this.GetCustomerDetails();
  } 

  GetCustomerDetails() {
    let Apiurl = 'https://localhost:7065/api/Customer';
    this.httpClient.get(Apiurl).subscribe(result => {
      this.CustomerDetails = result;
      console.log(result);
    });
  }

  onDeleteCustomer(customerId: number): void {
    const isdeleteConfirmed = confirm(`Are you sure you want to delete the customer with ID ${customerId}?`);
    if (isdeleteConfirmed) {
      this.deleteCustomer(customerId);
    }
  }

  deleteCustomer(customerId: number): void {
    const deleteUrl = `https://localhost:7065/api/Customer/${customerId}`;
    this.httpClient.delete(deleteUrl).subscribe({
      next: () => {
        console.log(`Customer with ID ${customerId} deleted successfully.`);
        this.GetCustomerDetails();
      },
      error: e => console.log(e)
    });
  }

}