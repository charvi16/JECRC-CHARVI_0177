import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './shared/navbar/navbar';

@Component({
  selector: 'app-root',
  standalone: true,   // ✅ important for standalone apps
  imports: [RouterOutlet, NavbarComponent],
  template: `
    <app-navbar></app-navbar>   <!-- ✅ Navbar visible on all pages -->

    <div style="padding:20px;">
      <router-outlet></router-outlet>   <!-- ✅ Route content loads here -->
    </div>
  `
})
export class App {
  title = 'employee-management';
}