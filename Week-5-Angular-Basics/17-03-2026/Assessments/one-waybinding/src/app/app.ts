import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { User } from './user/user';
import { Product } from './product/product';
import { Home } from './home/home';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, User, Product, Home],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('one-waybinding');
}
