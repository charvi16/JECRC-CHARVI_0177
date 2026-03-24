import { Injectable } from '@angular/core';
import { Course } from './course';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private courses: Course[] = [
    new Course(1, "Angular Mastery", 25, "Complete Angular from beginner to advanced"),
    new Course(2, "React Pro", 30, "Master React with hooks and advanced patterns"),
    new Course(3, "Full Stack Dev", 15, "Node + Angular + Database"),
    new Course(4, "Data Structures", 40, "Crack interviews with DSA")
  ];

  getCourses(): Course[] {
    return this.courses;
  }

  getCourseById(id: number): Course | undefined {
    return this.courses.find(c => c.CourseId === id);
  }

  enrollInCourse(id: number): boolean {
  const course = this.courses.find(c => c.CourseId === id);

  if (course && course.seatsVailable > 0) {
    course.seatsVailable--;
    return true;
  }

  return false;
}
}