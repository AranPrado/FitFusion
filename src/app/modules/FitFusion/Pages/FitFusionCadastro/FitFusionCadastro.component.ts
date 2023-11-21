import { Component, OnInit } from '@angular/core';
import { FitFusionCadastroModule } from './FitFusionCadastro-module';
import { Router } from '@angular/router';
import { RegistroModel } from '../../models/Models.model';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';


@Component({
  selector: 'app-FitFusionCadastro',
  templateUrl: './FitFusionCadastro.component.html',
  styleUrls: ['./FitFusionCadastro.component.css']
})
export class FitFusionCadastroComponent implements OnInit {

  frases: any = ["Transforme cada desafio em uma oportunidade.", "Pequenas mudanças podem fazer grandes diferenças.", "Acredite em si mesmo e tudo será possível."]

  fraseAtual: number = 0;
  etapaCadastro: number = 1;

  atualizarFrase() {
    this.fraseAtual = (this.fraseAtual + 1) % this.frases.length;
  }

  novoRegistro: RegistroModel = {
    nome: '',
    sobreNome: '',
    email: '',
    senha: '',
    confirmarSenha: '',
    peso: null,
    idade: null,
    altura: null,
    dataCriacao: new Date(),
    roleNome: null
  }

  constructor(private router: Router, private fitFusionService: FitFusionServicesService) { }

  ngOnInit() {
    setInterval(() => {
      this.atualizarFrase();
    }, 8000);
  }

  onProximoClick() {
    if (this.etapaCadastro === 1) {
      
      this.etapaCadastro = 2;
    } else if (this.etapaCadastro === 2) {
      
      this.registrarUsuario(); 
    }
  }

  onVoltarClick(){
    if(this.etapaCadastro === 2)
    {
      this.etapaCadastro = 1;
    } else {
      this.router.navigate(['/']);
    }

    }
  

  registrarUsuario() {
    this.fitFusionService.registro(this.novoRegistro).subscribe((res: any) => {
      console.log(res.message);
      this.router.navigate(['/']);
      
    },
    (error) => console.log('Error',error))
  }
}
