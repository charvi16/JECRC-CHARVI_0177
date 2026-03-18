import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StatusColorDirective } from './status-color';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, StatusColorDirective],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {

  students = [
    { name: 'Aarav', marks: 85 },
    { name: 'Diya', marks: 42 },
    { name: 'Kabir', marks: 67 },
    { name: 'Meera', marks: 30 },
    { name: 'Rohan', marks: 95 }
  ];
}