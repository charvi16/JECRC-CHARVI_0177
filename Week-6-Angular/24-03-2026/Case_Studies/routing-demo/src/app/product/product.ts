import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router'; // ✅ IMPORTANT
import { ProductService } from '../product.services';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule, RouterLink], // ✅ ADD THIS
  templateUrl: './product.html',
  styleUrls: ['./product.css']
})
export class ProductComponent implements OnInit {

  products: any[] = [];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.products = this.productService.getProducts();
  }
}