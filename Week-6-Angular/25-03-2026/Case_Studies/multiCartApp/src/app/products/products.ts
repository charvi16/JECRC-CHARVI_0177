import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartService } from '../cart.services';
import { Cart } from '../cart';

@Component({
  selector: 'app-products',
  imports: [CommonModule],
  templateUrl: './products.html',
  styleUrl: './products.css',
  providers : [CartService]
})
export class Products {
  products = [
    {id : 1, name : "laptop", price : 20000},
    {id : 2, name : "Mobile", price : 10000},
    {id : 3, name : "Desktop", price : 30000},
  ];

  constructor(private cartService : CartService){}

  addtoCart(product : any){
    this.cartService.addtoCart(product.name);
  }

  getCartItems(){
    return this.cartService.getItems();
  }
}
