import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-inventory',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './inventory.html',
  styleUrl: './inventory.css',
})
export class InventoryComponent implements OnInit {

  httpClient = inject(HttpClient);
  isupdate: boolean = false;

  private apiurl = 'https://localhost:7065/api/Inventory';

  inventoryData = {
    ProductID: 0,
    Productname: "",
    Avaliblestock: 0,
    Reorderstock: 0
  };

  inventoryList: any[] = [];

  private httpoptions = {
    headers: new HttpHeaders({
      Authorization: 'hello-auth-token',
      'Content-Type': 'application/json'
    })
  };

  ngOnInit(): void {
    this.loadInventory();
  }

  loadInventory(): void {
    this.httpClient.get<any[]>(this.apiurl, this.httpoptions).subscribe({
      next: data => {
        this.inventoryList = data;
        console.log(data);
      },
      error: e => console.log(e)
    });
  }

  onEdit(inventory: any): void {
    this.inventoryData.ProductID = inventory.productID;
    this.inventoryData.Productname = inventory.productname;
    this.inventoryData.Avaliblestock = inventory.avaliblestock;
    this.inventoryData.Reorderstock = inventory.reorderstock;
    this.isupdate = true;
  }

  onsubmit(): void {
    if (this.isupdate) {
      const updateUrl = `${this.apiurl}/${this.inventoryData.ProductID}`;
      this.httpClient.put(updateUrl, this.inventoryData, this.httpoptions).subscribe({
        next: v => console.log(v),
        error: e => console.log(e),
        complete: () => {
          alert('Inventory Updated Successfully');
          this.resetForm();
          this.loadInventory();
        }
      });
    } else {
      this.httpClient.post(this.apiurl, this.inventoryData, this.httpoptions).subscribe({
        next: v => console.log(v),
        error: e => console.log(e),
        complete: () => {
          alert('Form Submitted ' + JSON.stringify(this.inventoryData));
          this.resetForm();
          this.loadInventory();
        }
      });
    }
  }

  resetForm(): void {
    this.inventoryData = {
      ProductID: 0,
      Productname: "",
      Avaliblestock: 0,
      Reorderstock: 0
    };
    this.isupdate = false;
  }

  onDeleteInventory(productID: number): void {
    const isdeleteConfirmed = confirm(`Are you sure you want to delete the inventory with ProductID ${productID}?`);
    if (isdeleteConfirmed) {
      this.deleteInventory(productID);
    }
  }

  deleteInventory(productID: number): void {
    const deleteUrl = `${this.apiurl}/${productID}`;
    this.httpClient.delete(deleteUrl, this.httpoptions).subscribe({
      next: () => {
        console.log(`Inventory with ProductID ${productID} deleted successfully.`);
        this.loadInventory();
      },
      error: e => console.log(e)
    });
  }
}