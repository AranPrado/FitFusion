// Exemplo no serviço de autenticação (FitFusionServices.service.ts)
import { Injectable } from '@angular/core';
import { AuthGuard } from './auth.guard';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isAuthenticated(): boolean {
    // Verifica se existe um token no armazenamento local (ou outro local)
    const token = localStorage.getItem('token');

    // Se houver um token e ele for válido, considere o usuário autenticado
    return !!token;
  }
}
