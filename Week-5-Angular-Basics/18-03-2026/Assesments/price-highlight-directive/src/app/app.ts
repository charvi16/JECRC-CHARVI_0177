import { Component } from '@angular/core';
import { PriceHighlightDirective } from './highlight';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PriceHighlightDirective, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {

  products = [
    { name: 'iPhone', price: 80000 },
    { name: 'Headphones', price: 3000 },
    { name: 'Laptop', price: 75000 },
    { name: 'Mouse', price: 500 }
  ];
}