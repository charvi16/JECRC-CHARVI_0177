import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Product } from './product';

export interface CartItem extends Product {
  quantity: number;
}

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cartItems: CartItem[] = [];
  private cartItemsSubject = new BehaviorSubject<CartItem[]>([]);

  cartItems$ = this.cartItemsSubject.asObservable();

  private updateCartState(): void {
    this.cartItemsSubject.next([...this.cartItems]);
  }

  addToCart(product: Product): void {
    const existingItem = this.cartItems.find(
      item => item.productID === product.productID
    );

    if (existingItem) {
      existingItem.quantity++;
    } else {
      this.cartItems.push({ ...product, quantity: 1 });
    }

    this.updateCartState();
  }

  removeFromCart(productID: number): void {
    this.cartItems = this.cartItems.filter(item => item.productID !== productID);
    this.updateCartState();
  }

  increaseQuantity(productID: number): void {
    const item = this.cartItems.find(i => i.productID === productID);
    if (item) {
      item.quantity++;
      this.updateCartState();
    }
  }

  decreaseQuantity(productID: number): void {
    const item = this.cartItems.find(i => i.productID === productID);
    if (!item) return;

    if (item.quantity > 1) {
      item.quantity--;
    } else {
      this.cartItems = this.cartItems.filter(i => i.productID !== productID);
    }

    this.updateCartState();
  }

  clearCart(): void {
    this.cartItems = [];
    this.updateCartState();
  }

  getCurrentCartItems(): CartItem[] {
    return [...this.cartItems];
  }

  getTotal(): number {
    return this.cartItems.reduce(
      (total, item) => total + item.price * item.quantity,
      0
    );
  }

  getCount(): number {
    return this.cartItems.reduce((count, item) => count + item.quantity, 0);
  }
}