import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, inject, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-customer-detail-dialog',
  imports: [FormsModule, CommonModule],
  templateUrl: './customer-detail-dialog.html',
  styleUrl: './customer-detail-dialog.css',
})
export class CustomerDetailDialog {
  modal = inject(NgbActiveModal);
  httpClient = inject(HttpClient);

  @Input() isUpdate: boolean = false;

  CustomerDetails = {
    CustomerId: '',
    CustomerName: '',
    CustomerEmail: '',
    CustomerPhone: '',
    RegistrationDate: ''
  };

  private httpOptions = {
    headers: new HttpHeaders({
      Authorization: 'umar-auth-token',
      'Content-Type': 'application/json'
    }),
    responseType: 'text' as 'json'
  };

  onSubmit() {
    if (this.isUpdate) {
      let Apiurl = 'https://localhost:7065/api/Customer/' + this.CustomerDetails.CustomerId;
      this.httpClient.put(Apiurl, this.CustomerDetails, this.httpOptions).subscribe({
        next: v => console.log(v),
        error: e => console.error(e),
        complete: () => {
          console.log('Customer updated successfully.' + JSON.stringify(this.CustomerDetails));
          this.modal.close('Customer updated successfully.');
        }
      });
    } else {
      let Apiurl = 'https://localhost:7065/api/Customer';
      this.httpClient.post(Apiurl, this.CustomerDetails, this.httpOptions).subscribe({
        next: v => console.log(v),
        error: e => console.error(e),
        complete: () => {
          console.log('Customer added successfully.' + JSON.stringify(this.CustomerDetails));
          this.modal.close('Customer added successfully.');
        }
      });
    }
  }
}