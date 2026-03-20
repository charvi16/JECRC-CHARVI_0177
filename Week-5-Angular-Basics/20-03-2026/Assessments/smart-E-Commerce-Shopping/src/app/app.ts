import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductComponent } from './product/product';
import { ProductDetailsComponent } from './product-details-component/product-details-component';
import { CartComponent } from './cart/cart';
import { CheckoutComponent } from './checkout/checkout';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ProductComponent,
    ProductDetailsComponent,
    CartComponent,
    CheckoutComponent
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  currentPage: string = 'products';
  selectedProduct: any = null;
  searchText: string = '';

  products = [
    {
      id: 1,
      name: 'Apple iPhone 15',
      price: 69999,
      category: 'Mobiles',
      rating: 4.5,
      image: 'https://images.unsplash.com/photo-1695048133142-1a20484d2569?auto=format&fit=crop&w=800&q=80',
      description: 'Powerful smartphone with excellent camera and premium build quality.'
    },
    {
      id: 2,
      name: 'Sony WH-1000XM5',
      price: 24999,
      category: 'Electronics',
      rating: 4.7,
      image: 'https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=800&q=80',
      description: 'Industry-leading noise cancelling headphones with rich sound.'
    },
    {
      id: 3,
      name: 'Nike Running Shoes',
      price: 4999,
      category: 'Fashion',
      rating: 4.3,
      image: 'https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=800&q=80',
      description: 'Comfortable and stylish running shoes for daily wear.'
    },
    {
      id: 4,
      name: 'Dell Inspiron Laptop',
      price: 55999,
      category: 'Computers',
      rating: 4.4,
      image: 'https://images.unsplash.com/photo-1496181133206-80ce9b88a853?auto=format&fit=crop&w=800&q=80',
      description: 'Reliable performance laptop for study, office work, and entertainment.'
    }
  ];

  cart: any[] = [];

  goTo(page: string) {
    this.currentPage = page;
  }

  viewProduct(product: any) {
    this.selectedProduct = product;
    this.currentPage = 'details';
  }

  addToCart(product: any) {
    const existing = this.cart.find((item) => item.id === product.id);

    if (existing) {
      existing.quantity++;
    } else {
      this.cart.push({ ...product, quantity: 1 });
    }

    alert(`${product.name} added to cart`);
  }

  updateQuantity(item: any, change: number) {
    item.quantity += change;
    if (item.quantity <= 0) {
      this.removeItem(item.id);
    }
  }

  removeItem(id: number) {
    this.cart = this.cart.filter((item) => item.id !== id);
  }

  clearCart() {
    this.cart = [];
  }

  getCartCount() {
    return this.cart.reduce((sum, item) => sum + item.quantity, 0);
  }
}