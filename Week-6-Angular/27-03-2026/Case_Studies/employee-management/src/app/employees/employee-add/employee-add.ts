import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EmployeeService } from '../../core/services/employee.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-add',
  standalone: true,
  imports: [FormsModule],
  template: `
    <h2>Add Employee</h2>

    <input [(ngModel)]="employee.name" placeholder="Name">
    <input [(ngModel)]="employee.role" placeholder="Role">

    <button (click)="addEmployee()">Add</button>
  `
})
export class EmployeeAdd {

  employee = {
    id: 0,
    name: '',
    role: ''
  };

  constructor(private service: EmployeeService, private router: Router) {}

  addEmployee() {
    this.employee.id = Date.now(); // simple unique id
    this.service.addEmployee(this.employee);
    this.router.navigate(['/employees']);
  }
}