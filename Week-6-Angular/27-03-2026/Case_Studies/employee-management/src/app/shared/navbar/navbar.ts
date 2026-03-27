import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [RouterModule],
  standalone: true,
//   templateUrl: './navbar.html',
//   styleUrl: './navbar.css',
template : 
`
  <nav>
    <a routerLink = "/employees">Employees</a>
    <a routerLink = "/login">Login</a>
  </nav>
`
})
export class NavbarComponent {

}
