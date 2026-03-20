import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FeedbackForm } from './feedback-form/feedback-form';
import { EmployeeForm } from "./employee-form/employee-form";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, FeedbackForm, EmployeeForm],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('form-demo');
}
