import { Component } from '@angular/core';
import { EmployeeService } from '../../core/services/employee.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-employee-list',
  imports: [CommonModule, RouterModule],
  standalone: true,
  // templateUrl: './employee-list.html',
  // styleUrl: './employee-list.css',
  template:
  `
    <ul>
  <li *ngFor="let emp of employees">
    {{ emp.name }} - {{ emp.role }}

    <button [routerLink]="['/view', emp.id]">View</button>
    <button [routerLink]="['/edit', emp.id]">Edit</button>
    <button (click)="delete(emp.id)">Delete</button>
  </li>
</ul>

<button routerLink="/add">Add Employee</button>
  `
})

export class EmployeeList {
  employees : any[] = [];

  constructor(private service : EmployeeService){
    this.employees = this.service.getEmployees();
  };

  delete(id: number) {
  this.service.deleteEmployee(id);
  this.employees = this.service.getEmployees(); // refresh list
}

}
