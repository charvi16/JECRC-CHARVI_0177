import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
// import { App } from './app/app';
import { ComponentTrial } from './component-trial/component-trial';



@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [RouterOutlet, ComponentTrial],
})
export class App {
  protected readonly title = signal('Angular Standalone App');
  user = {"name" : "charss", "age" : 10};

}
