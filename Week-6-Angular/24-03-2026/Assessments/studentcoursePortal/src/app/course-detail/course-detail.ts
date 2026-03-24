import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CourseService } from '../course.services';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-course-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './course-detail.html',
  styleUrl: './course-detail.css'
})
export class CourseDetail {

  course: any;
  message = "";

  constructor(
    private route: ActivatedRoute,
    private courseService: CourseService
  ) {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.course = this.courseService.getCourseById(id);
  }

  enroll() {
    const success = this.courseService.enrollInCourse(this.course.CourseId);

    if (success) {
      this.message = "✅ Successfully Enrolled!";
    } else {
      this.message = "❌ No seats available!";
    }
  }
}