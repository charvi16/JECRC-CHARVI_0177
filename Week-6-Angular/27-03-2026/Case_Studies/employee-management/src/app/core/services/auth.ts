import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthServices {
  isLoggedIn = false;

  username: string = '';
  password: string = '';

  login(username : string, password : string): boolean{
    if(username === 'admin' && password === 'admin'){
      this.isLoggedIn = true;
      localStorage.setItem('token', 'dummy.token');
      return true;
    }
    return false;
  }

  logout(){
    this.isLoggedIn = false;
    localStorage.removeItem('token');
  }

  isAuthenticated() : boolean{
    return !!localStorage.getItem('token');
  }
}
