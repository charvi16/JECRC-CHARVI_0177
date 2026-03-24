import { Injectable } from '@angular/core';
import { Product } from './product';

@Injectable({
  providedIn: 'root',
})
export class ProductService{
  getProducts(){
    return[
      new Product(1, 'Laptop', 90000),
      new Product(2, 'Mobile', 50000),
      new Product(3, 'HeadPhones', 2000),
    ]
  }

  getProductById(ProductID : number) : Product | undefined{
    return this.getProducts().find(p => p.ProductID === ProductID);
  }
}
