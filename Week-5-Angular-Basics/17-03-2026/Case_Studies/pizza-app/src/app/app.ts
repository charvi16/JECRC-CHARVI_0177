import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Pizza } from './pizza/pizza';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Pizza],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('pizza-app');
}
