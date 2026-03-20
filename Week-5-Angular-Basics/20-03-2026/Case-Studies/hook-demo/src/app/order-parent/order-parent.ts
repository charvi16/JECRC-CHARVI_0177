import { Component } from '@angular/core';
// import { CommonModule, NgIf } from "../../../node_modules/@angular/common/common_module.d-NEF7UaHr";
import { CommonModule } from '@angular/common';
import { OrderChildComponent } from '../order-child/order-child';

@Component({
  selector: 'app-order-parent',
  standalone: true,
  imports: [CommonModule, OrderChildComponent],
  templateUrl: './order-parent.html',
  styleUrls: ['./order-parent.css']
})
export class OrderParentComponent {
    order = {
      id : 101, 
      product: 'Laptop',
      status: 'Pending',
      price: 50000
    };

    updateOrder()
    {
      this.order = {
        ...this.order, 
        status: this.order.status === 'Pending' ? 'Delivered' : 'Pending'
      };
    }

    destroyChild = true;

    toggleChild(){
      this.destroyChild = !this.destroyChild;
    }
}