import { Component, OnDestroy, OnInit } from '@angular/core';
import { CartItem, CartService } from '../cart.service';
import { CommonModule } from '@angular/common';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart.html',
  styleUrls: ['./cart.css'],
})
export class Cart implements OnInit, OnDestroy {
  cartItems: CartItem[] = [];
  private destroy$ = new Subject<void>();

  constructor(public cartService: CartService) {}

  ngOnInit(): void {
    this.cartService.cartItems$
      .pipe(takeUntil(this.destroy$))
      .subscribe(items => {
        this.cartItems = items;
      });
  }

  get total(): number {
    return this.cartService.getTotal();
  }

  increase(productID: number): void {
    this.cartService.increaseQuantity(productID);
  }

  decrease(productID: number): void {
    this.cartService.decreaseQuantity(productID);
  }

  remove(productID: number): void {
    this.cartService.removeFromCart(productID);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}