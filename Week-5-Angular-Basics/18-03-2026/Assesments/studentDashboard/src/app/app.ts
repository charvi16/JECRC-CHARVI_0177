import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  students = [
    { name: 'Aarav', marks: 95, grade: 'A' },
    { name: 'Diya', marks: 82, grade: 'B' },
    { name: 'Kabir', marks: 67, grade: 'C' },
    { name: 'Meera', marks: 45, grade: 'D' },
    { name: 'Rohan', marks: 28, grade: 'F' }
  ];
}