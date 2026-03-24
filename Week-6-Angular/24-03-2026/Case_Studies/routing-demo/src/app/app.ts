import { Component, signal } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Product } from './product';
import { Error } from './error/error';
import { Home } from './home/home';
import { Contact } from './contact/contact';
import { ProductComponent } from './product/product';
import { CommonModule } from '@angular/common';
import { routes } from '../app/app.routes';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, CommonModule],
  // templateUrl: './app.html',
  template : `
  <h1>Angular Routing</h1>
  <nav>
    <a routerLink="/home">Home</a>
    <a routerLink="/contact">Contact</a>
    <a routerLink="/products">Product</a>
    <router-outlet></router-outlet>
  </nav>
  `,
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('routing-demo');
}
