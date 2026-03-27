import { Routes } from '@angular/router';
import { NavbarComponent } from './shared/navbar/navbar';
import { Login } from './auth/login/login';
import { authGuard } from './core/gaurds/auth-guard';
import { EmployeeList } from './employees/employee-list/employee-list';
import { EmployeeAdd } from './employees/employee-add/employee-add';
import { EmployeeEdit } from './employees/employee-edit/employee-edit';
import { EmployeeView } from './employees/employee-view/employee-view';

export const routes: Routes = [
    { path: 'employees', component: EmployeeList },
  { path: 'add', component: EmployeeAdd },
  { path: 'edit/:id', component: EmployeeEdit },
  { path: 'view/:id', component: EmployeeView },
    {path : 'login', component : Login},
    {path : 'employees', component : EmployeeList, canActivate: [authGuard]},
    {path : '', pathMatch: 'full', redirectTo: 'login'},


];
