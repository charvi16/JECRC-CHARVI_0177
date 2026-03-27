import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../../core/services/employee.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-employee-edit',
  standalone: true,
  imports: [FormsModule],
  template: `
    <h2>Edit Employee</h2>

    <input [(ngModel)]="employee.name">
    <input [(ngModel)]="employee.role">

    <button (click)="updateEmployee()">Update</button>
  `
})
export class EmployeeEdit implements OnInit {

  employee: any = {};

  constructor(
    private route: ActivatedRoute,
    private service: EmployeeService,
    private router: Router
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.employee = this.service.getEmployee(id);
  }

  updateEmployee() {
    this.service.updateEmployee(this.employee);
    this.router.navigate(['/employees']);
  }
}