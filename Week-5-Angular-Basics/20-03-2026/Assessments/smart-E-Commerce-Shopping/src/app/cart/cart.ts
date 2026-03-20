import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart.html',
  styleUrls: ['./cart.css']
})
export class CartComponent {
  @Input() cart: any[] = [];

  @Output() remove = new EventEmitter<number>();
  @Output() update = new EventEmitter<any>();
  @Output() clear = new EventEmitter<void>();
  @Output() checkout = new EventEmitter<void>();

  getTotal() {
    return this.cart.reduce((sum, item) => sum + item.price * item.quantity, 0);
  }
}