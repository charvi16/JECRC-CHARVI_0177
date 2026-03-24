import { Component } from '@angular/core';

@Component({
  selector: 'app-profile',
  standalone: true,
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})
export class Profile {
  student = {
    name: "Charvi",
    email: "charvi@email.com",
    enrolledCourses: 2
  };
}