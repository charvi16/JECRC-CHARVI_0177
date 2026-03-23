import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CartService } from '../cart.service';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './checkout.html',
  styleUrls: ['./checkout.css'],
})
export class Checkout {
  form = {
    name: '',
    email: '',
    address: '',
    payment: 'UPI'
  };

  constructor(private cartService: CartService) {}

  submit(): void {
    if (!this.form.name || !this.form.email || !this.form.address || !this.form.payment) {
      alert('Please fill all checkout details.');
      return;
    }

    alert(`Order Placed Successfully!\n\n${JSON.stringify(this.form, null, 2)}`);
    console.log('Order Details:', this.form);

    this.cartService.clearCart();

    this.form = {
      name: '',
      email: '',
      address: '',
      payment: 'UPI'
    };
  }
}