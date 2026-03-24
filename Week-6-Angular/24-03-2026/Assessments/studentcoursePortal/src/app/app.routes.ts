import { Routes } from '@angular/router';
import { Dashboard } from './dashboard/dashboard';
import { Profile } from './profile/profile';
import { CourseComponent } from './courses/courses';
import { Error } from './error/error';
import { CourseDetail } from './course-detail/course-detail';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },

  { path: 'dashboard', component: Dashboard },

  { path: 'profile', component: Profile },

  { path: 'courses', component: CourseComponent }, // ✅ FIXED

  { path: 'course/:id', component: CourseDetail }, // ✅ FIXED

  { path: '**', component: Error }
];