import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { InventoryComponent } from './pages/inventory/inventory';
import { Customers } from './pages/customers/customers';
import { Bill } from './pages/bill/bill';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: Home },
  { path: 'inventory', component: InventoryComponent },
  { path: 'customers', component: Customers },
  { path: 'bill', component: Bill },
];