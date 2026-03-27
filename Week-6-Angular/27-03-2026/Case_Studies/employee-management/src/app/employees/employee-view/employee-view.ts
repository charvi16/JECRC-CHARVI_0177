import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../../core/services/employee.service';

@Component({
  selector: 'app-employee-view',
  standalone: true,
  template: `
    <h2>Employee Details</h2>

    <p><b>Name:</b> {{ employee?.name }}</p>
    <p><b>Role:</b> {{ employee?.role }}</p>
  `
})
export class EmployeeView implements OnInit {

  employee: any;

  constructor(
    private route: ActivatedRoute,
    private service: EmployeeService
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.employee = this.service.getEmployee(id);
  }
}