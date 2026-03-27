import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthServices } from '../../core/services/auth';


@Component({
  selector: 'app-login',
  imports: [FormsModule],
  standalone : true,
  // templateUrl: './login.html',
  // styleUrl: './login.css',
  template :
  `
    <h2>
      <input [(ngModel)]="username" placeholder="Username">
      <input [(ngModel)]="password" placeholder="Password" type="password">
      <button (click)="login()">Login</button>
    </h2>
  `
})
export class Login {
  username: string = '';
  password: string = '';
  constructor(private auth : AuthServices, private router : Router){};

  login(){
    if (this.auth.login(this.username, this.password)) {
      this.router.navigate(['/employees']);
    }
    else{
      alert('Invalid Credentials');
    }
  }

}
