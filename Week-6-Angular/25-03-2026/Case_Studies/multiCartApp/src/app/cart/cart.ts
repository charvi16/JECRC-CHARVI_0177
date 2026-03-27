import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartService } from '../cart.services';


@Component({
  selector: 'app-cart',
  imports: [CommonModule],
  templateUrl: './cart.html',
  styleUrl: './cart.css',
  providers : [CartService]
})
export class Cart {
  constructor(private cartService : CartService){}

  getCartItems(){
    return this.cartService.getItems();
  }

  addToCart(product : any){
    this.cartService.addtoCart(product.name);
  }
}
