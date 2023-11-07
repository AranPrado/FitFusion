import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginModel, RegistroModel } from '../models/LoginModel.model';

@Injectable({
  providedIn: 'root'
})
export class FitFusionServicesService {

private apiUrl = 'http://localhost:5166';

constructor(private http: HttpClient) { }

login(loginModel: LoginModel) {
  return this.http.post(`${this.apiUrl}/api/Autoriza/Login`, loginModel);
}

registro(registroModel: RegistroModel){
  return this.http.post(`${this.apiUrl}/api/Autoriza/register`, registroModel);
}

}
