import { Component, OnInit } from '@angular/core';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import { library } from '@fortawesome/fontawesome-svg-core';

import { FitFusionServicesService } from '../../services/FitFusionServices.service';
import { FitFusionLoginModule } from './FitFusionLogin-module';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { Router } from '@angular/router';
import { LoginModel } from '../../models/Models.model';




@Component({
  selector: 'app-FitFusionLogin',
  templateUrl: './FitFusionLogin.component.html',
  styleUrls: ['./FitFusionLogin.component.css'],
  
})
export class FitFusionLoginComponent implements OnInit {

  frases: any = ["Transforme cada desafio em uma oportunidade.", "Pequenas mudanças podem fazer grandes diferenças.", "Acredite em si mesmo e tudo será possível."]

  fraseAtual: number = 0;

  atualizarFrase() {
    this.fraseAtual = (this.fraseAtual + 1) % this.frases.length;
  }
  
  novoLogin: LoginModel = {
    email: '',
    senha: ''
  };
  userId: string | null | undefined;
  

  constructor(private fitFusionService: FitFusionServicesService,private router: Router) {
    
   }

  login():void{
    this.fitFusionService.login(this.novoLogin).subscribe((res: any) => {
     
      

      localStorage.setItem('token', res.token);
      localStorage.setItem('userId', res.aspNetUserID);
      
      
      
      this.router.navigate(['/index']);
    },
    (error) => {console.error('Error', error)});
  };

  ngOnInit() {
    setInterval(() => {
      this.atualizarFrase();
    }, 8000);
    this.userId = localStorage.getItem('userId');
  }

  
}
