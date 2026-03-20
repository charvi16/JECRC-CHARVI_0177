import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderParentComponent } from './order-parent/order-parent'

@Component({
  selector: 'app-root',
  imports: [CommonModule, OrderParentComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('hook-demo');
}
