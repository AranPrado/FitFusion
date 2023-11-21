// auth.guard.ts
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

import { AuthService } from './auth-services';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    // Verifique se o usuário está autenticado usando a lógica no AuthService
    const isAuthenticated = this.authService.isAuthenticated();

    if (isAuthenticated) {
      return true; // O acesso é permitido
    } else {
      // Redirecione para a página de login se não estiver autenticado
      this.router.navigate(['/FitFusion']);
      return false; // O acesso é negado
    }
  }
}
