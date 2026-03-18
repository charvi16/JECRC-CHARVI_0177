import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('usingDirectives');
  showProducts = true;

  products = [
    {name : "Laptop", price : 300000, status : "available"},
    {name : "Mobile", price : 1000, status : "out"},
    {name : "Ipad", price : 300000, status : "limited"}
  ]
}
