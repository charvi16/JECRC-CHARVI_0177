import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './checkout.html',
  styleUrls: ['./checkout.css']
})
export class CheckoutComponent {
  @Input() cart: any[] = [];

  user: any = {
    fullName: '',
    addressLine: '',
    email: '',
    phoneNumber: '',
    zipCode: '',
    gender: '',
    deliveryType: 'Standard',
    acceptTerms: false,
    subscribeOffers: false,
    city: '',
    state: '',
    country: '',
    deliveryDate: '',
    instructions: '',
    idProof: '',
    addresses: [''],
    paymentMethods: [''],
    paymentType: '',
    cardNumber: '',
    expiryDate: '',
    cvv: '',
    upiId: ''
  };

  onFileSelected(event: any) {
    const file = event.target.files[0];
    this.user.idProof = file ? file.name : '';
  }

  addAddress() {
    this.user.addresses.push('');
  }

  addPaymentMethod() {
    this.user.paymentMethods.push('');
  }

  getCartTotal() {
    return this.cart.reduce((sum, item) => sum + item.price * item.quantity, 0);
  }

  placeOrder() {
    const orderData = {
      customerDetails: this.user,
      cartItems: this.cart,
      totalAmount: this.getCartTotal(),
      placedAt: new Date().toLocaleString()
    };

    alert(JSON.stringify(orderData, null, 2));
  }
}