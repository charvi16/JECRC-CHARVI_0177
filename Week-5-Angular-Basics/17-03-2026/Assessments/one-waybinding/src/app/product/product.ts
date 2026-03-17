import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product',
  imports: [CommonModule],
  templateUrl: './product.html',
  styleUrl: './product.css',
})
export class Product {
  title = "My Products";
  products = [
    {"Name" : "TV", "Price" : 20000},
    {"Name" : "laptop", "Price" : "30000"},
    {"Name" : "mobile", "Price" : "100000"}
  ];

  product = {"Name" : "bottle", "Price" : 1000};
  getNamePrice(){
    return "items : " + this.product;
  }
}
