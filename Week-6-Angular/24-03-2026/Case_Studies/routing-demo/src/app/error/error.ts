import { Component } from '@angular/core';

@Component({
  selector: 'app-error',
  imports: [],
  standalone : true,
  // templateUrl: './error.html',
  template : `
  <h1>404-page not found</h1>
  <p>error page</p>`,
  styleUrl: './error.css',
})
export class Error {

}
