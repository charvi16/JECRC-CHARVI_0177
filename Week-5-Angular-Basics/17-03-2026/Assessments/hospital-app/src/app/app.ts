import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PatientDetails } from './patient-details/patient-details';

@Component({
  selector: 'app-root',
  imports: [PatientDetails],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('hospital-app');
}
