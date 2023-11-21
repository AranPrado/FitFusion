import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginModel, PerfilModel, RegistroModel } from '../models/Models.model';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, retry } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FitFusionServicesService {

private apiUrl = 'http://localhost:5166';

constructor(private http: HttpClient) { }

httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
}

login(loginModel: LoginModel) {
  return this.http.post(`${this.apiUrl}/api/Autoriza/Login`, loginModel);
}

registro(registroModel: RegistroModel){
  return this.http.post(`${this.apiUrl}/api/Autoriza/register`, registroModel);
}

private getAuthorizedHeaders(): HttpHeaders {
  const token = localStorage.getItem('token');
  return new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `${token}`
  });
}


informacoesUsuario(id: any): Observable<PerfilModel> {
  // Construa a URL com o id diretamente incluído
  const url = `${this.apiUrl}/api/Usuario/InformacoesUsuario/${id}`;

  // Obtenha os cabeçalhos autorizados
  const headers = this.getAuthorizedHeaders();

  // Faça a chamada HTTP
  return this.http.get<PerfilModel>(url, { headers: headers });
}


}
