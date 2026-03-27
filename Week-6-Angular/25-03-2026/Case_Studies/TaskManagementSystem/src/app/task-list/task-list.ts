import { Component, OnInit } from '@angular/core';
import { TaskService } from '../tasks.services';
import { Task } from '../task';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TaskForm } from '../task-form/task-form';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, FormsModule, TaskForm],
  templateUrl: './task-list.html',
  styleUrl: './task-list.css'
})
export class TaskList implements OnInit {
  tasks: Task[] = [];
  searchTerm: string = '';

  constructor(private taskService: TaskService) {}

  ngOnInit() {
    this.loadTasks();
  }

  loadTasks() {
    this.taskService.getTasks().subscribe((data) => {
      this.tasks = data.slice(0, 10); // limit for UI
    });
  }

  addTask(task: Task) {
    this.tasks.unshift(task);
  }

  toggleTask(task: Task) {
    this.taskService
      .updateTaskStatus(task.id!, !task.completed)
      .subscribe(() => {
        task.completed = !task.completed;
      });
  }

  deleteTask(id: number) {
    this.taskService.deleteTask(id).subscribe(() => {
      this.tasks = this.tasks.filter((t) => t.id !== id);
    });
  }

  search() {
  console.log("Search term:", this.searchTerm);

  if (!this.searchTerm.trim()) {
    this.loadTasks();
    return;
  }

  this.taskService.searchTask(this.searchTerm).subscribe((res) => {
    console.log("Search result:", res);
    this.tasks = res;
  });


    this.taskService.searchTask(this.searchTerm).subscribe((res) => {
      this.tasks = res;
    });
  }
}