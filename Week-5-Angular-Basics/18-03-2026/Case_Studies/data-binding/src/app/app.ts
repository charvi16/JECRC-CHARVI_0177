import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('data-binding');
  productName = "Laptop";
  price = 50000;
  quantity = 1;
  isAvailable = true;

  imageUrl = 'https://picsum.photos/150';

  customerName = '';
  address = '';

  increaseQty(){
    this.quantity++;
  }

  decreaseQty(){
    if(this.quantity > 1)
    this.quantity--;
  }

  toggleAvailable(){
    this.isAvailable = !this.isAvailable;
  }

  getTotal(){
    return this.price * this.quantity;
  }

  
}
