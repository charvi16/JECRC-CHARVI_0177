import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user',
  imports: [CommonModule],
  templateUrl: './user.html',
  styleUrl: './user.css',
})
export class User {
  title = "My App";
  users = [
    "john",
    "alex",
    "aiden",
    "darrel"
  ];
  user = {name : "john", age : 21};
  getGreeting(){
    return "Welcome to Angular" + this.user;
  }
}
