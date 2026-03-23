import { Injectable } from '@angular/core';
import { Product } from './product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  getProducts(): Product[] {
    return [
      {
        productID: 1,
        name: 'Laptop Pro X',
        price: 999.99,
        image: 'https://images.unsplash.com/photo-1496181133206-80ce9b88a853?q=80&w=1200&auto=format&fit=crop',
        category: 'Electronics',
        rating: 4.7
      },
      {
        productID: 2,
        name: 'Smartphone Elite',
        price: 549.99,
        image: 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?q=80&w=1200&auto=format&fit=crop',
        category: 'Mobiles',
        rating: 4.5
      },
      {
        productID: 3,
        name: 'Wireless Headphones',
        price: 123.99,
        image: 'https://images.unsplash.com/photo-1505740420928-5e560c06d30e?q=80&w=1200&auto=format&fit=crop',
        category: 'Audio',
        rating: 4.3
      }
    ];
  }
}