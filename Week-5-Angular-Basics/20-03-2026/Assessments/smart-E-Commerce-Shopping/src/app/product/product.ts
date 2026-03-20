import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product.html',
  styleUrls: ['./product.css']
})
export class ProductComponent {
  @Input() products: any[] = [];
  @Input() searchText: string = '';

  @Output() add = new EventEmitter<any>();
  @Output() view = new EventEmitter<any>();

  get filteredProducts() {
    return this.products.filter((product) =>
      product.name.toLowerCase().includes(this.searchText.toLowerCase()) ||
      product.category.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }
}