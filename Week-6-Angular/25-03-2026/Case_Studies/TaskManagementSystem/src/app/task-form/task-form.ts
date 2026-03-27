import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TaskService } from '../tasks.services';
import { Task } from '../task';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './task-form.html',
  styleUrl: './task-form.css'
})
export class TaskForm {
  title: string = '';

  @Output() taskAdded = new EventEmitter<Task>();

  constructor(private taskService: TaskService) {}

  addTask() {
    if (!this.title.trim()) return;

    const newTask: Task = {
      title: this.title,
      completed: false
    };

    this.taskService.addTask(newTask).subscribe((res) => {
      this.taskAdded.emit(res);
      this.title = '';
    });
  }
}