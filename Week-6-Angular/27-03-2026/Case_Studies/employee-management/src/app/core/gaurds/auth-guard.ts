import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthServices } from '../services/auth';

export const authGuard: CanActivateFn = () => {
  
  const auth = inject(AuthServices);
  const router = inject(Router);

  if(auth.isAuthenticated()){
    return true;
  }

  router.navigate(['/login']);
  return false;
};
