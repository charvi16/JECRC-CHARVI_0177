import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-details-component.html',
  styleUrls: ['./product-details-component.css']
})
export class ProductDetailsComponent {
  @Input() product: any;
  @Output() back = new EventEmitter<void>();
  @Output() add = new EventEmitter<any>();
}