import { Component, OnInit } from '@angular/core';
import { FitFusionIndexModule } from './FItFusionIndex-module';
import { PerfilModel } from '../../models/Models.model';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-FitFusionIndex',
  templateUrl: './FitFusionIndex.component.html',
  styleUrls: ['./FitFusionIndex.component.css']
})
export class FitFusionIndexComponent implements OnInit {

  perfilModel: PerfilModel | undefined;
  userId: string | null | undefined;

  constructor(private fitFusionService: FitFusionServicesService, private router: Router) { }

  ngOnInit() {
    // Obtenha o userId do localStorage
    const userId = localStorage.getItem('userId');
  
    if (userId) {
        // Chame o método informacoesUsuario com o userId obtido
        this.fitFusionService.informacoesUsuario(userId).subscribe(
            (perfil: PerfilModel) => {
                // Faça algo com os dados do perfil, por exemplo, atribua a uma propriedade do componente
                console.log(perfil);
            },
            (error) => {
                console.error('Erro ao obter informações do usuário', error);
            }
        );
    } else {
        console.error('userId não encontrado no localStorage');
    }
  }
  
  logout() {
    // Limpe as informações de autenticação
    localStorage.removeItem('userId');
    localStorage.removeItem('token');
    
    // Redirecione para a página de login ou onde for apropriado
    this.router.navigate(['/']);
  }

}
