import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClickBlockDirective } from './click-block';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, ClickBlockDirective],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {

  allowAction = false;

  toggleAccess() {
    this.allowAction = !this.allowAction;
  }

  performAction() {
    alert('Action executed ✅');
  }
}