import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../product.services';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="card" *ngIf="product">
      <h2>{{product.name}}</h2>
      <p>ID: {{product.ProductID}}</p>
      <p>Price: {{product.price}}</p>
    </div>
  `,
  styleUrls: ['./product-detail.css'],
})
export class ProductDetail implements OnInit {

  product: any;

  constructor(
    private route: ActivatedRoute,
    private service: ProductService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.product = this.service.getProductById(id);
  }
}