import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Rxjs } from './rxjs/rxjs';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Rxjs],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('rxjs-Demo');
}
