import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Product } from '../product';

@Component({
  selector: 'app-product-inventory',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './product-inventory.html',
  styleUrl: './product-inventory.css'
})
export class ProductInventoryComponent implements OnInit {
  // Original full list of products
  products: Product[] = [];

  // Products currently visible in the table
  displayedProducts: Product[] = [];

  // Dropdown selected category
  selectedCategory: string = '';

  // Checkbox state
  inStockOnly: boolean = false;

  // Used to populate dropdown options
  categories: string[] = [];

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts(): void {
    this.products = [
      { id: 1, name: 'Laptop', category: 'Electronics', price: 850.50, stock: 10 },
      { id: 2, name: 'Phone', category: 'Electronics', price: 599.99, stock: 0 },
      { id: 3, name: 'Desk Chair', category: 'Furniture', price: 120.00, stock: 5 },
      { id: 4, name: 'Notebook', category: 'Stationery', price: 5.99, stock: 50 },
      { id: 5, name: 'Pen Pack', category: 'Stationery', price: 3.49, stock: 0 },
      { id: 6, name: 'Table', category: 'Furniture', price: 220.75, stock: 2 }
    ];

    this.displayedProducts = [...this.products];

    this.categories = [...new Set(this.products.map(product => product.category))];
  }

  filterProducts(): void {
    let filtered = [...this.products];

    // Apply category filter only if user selected one
    if (this.selectedCategory) {
      filtered = filtered.filter(
        product => product.category === this.selectedCategory
      );
    }

    // Apply stock filter if checkbox is checked
    if (this.inStockOnly) {
      filtered = filtered.filter(
        product => product.stock > 0
      );
    }

    this.displayedProducts = filtered;
  }

  sortByPrice(): void {
    this.displayedProducts = [...this.displayedProducts].sort(
      (a, b) => a.price - b.price
    );
  }

  onStockToggle(): void {
    this.filterProducts();
  }
}