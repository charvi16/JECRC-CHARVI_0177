import { Component } from '@angular/core';
import { ProductInventoryComponent } from './product-inventory/product-inventory';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ProductInventoryComponent],
  template: `<app-product-inventory></app-product-inventory>`
})
export class App {}