import { Component, signal } from '@angular/core';
import { Cart } from './cart/cart';
import { Checkout } from './checkout/checkout';
import { ProductList } from './product-list/product-list';

@Component({
  selector: 'app-root',
  standalone: true,  // ✅ VERY IMPORTANT
  imports: [Cart, Checkout, ProductList],
  templateUrl: './app.html',
  styleUrls: ['./app.css']  // ✅ fixed
})
export class App {
  protected readonly title = signal('ecommerce-app');
}