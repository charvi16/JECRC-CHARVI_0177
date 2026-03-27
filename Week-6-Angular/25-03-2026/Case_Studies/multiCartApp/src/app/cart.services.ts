import { Injectable } from '@angular/core';

@Injectable()
export class CartService {
private item : string [] = [];

addtoCart(item : string){
  this.item.push(item);
}

  getItems(){
    return this.item;
  }

  clearCart(){
    this.item = [];
  }
}
