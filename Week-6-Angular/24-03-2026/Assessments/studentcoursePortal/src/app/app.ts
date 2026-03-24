import { Component, signal } from '@angular/core';
import { RouterModule} from '@angular/router';
import { CourseComponent } from './courses/courses';
import { Dashboard } from './dashboard/dashboard';
import { Profile } from './profile/profile';

@Component({
  selector: 'app-root',
  imports: [RouterModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('studentcoursePortal');
}
