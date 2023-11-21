import { Component, Input, OnInit } from '@angular/core';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';
import { PerfilModel } from '../../models/Models.model';



@Component({
  selector: 'app-CardPerfil',
  templateUrl: './CardPerfil.component.html',
  styleUrls: ['./CardPerfil.component.css']
})
export class CardPerfilComponent implements OnInit {

  perfilModel?: PerfilModel;


  constructor(private fitFusionService: FitFusionServicesService) { }

  ngOnInit() {
    // Obtenha o userId do localStorage
    const userId = localStorage.getItem('userId');
  
    if (userId) {
        // Chame o método informacoesUsuario com o userId obtido
        this.fitFusionService.informacoesUsuario(userId).subscribe(
            (perfil: PerfilModel) => {
                // Faça algo com os dados do perfil, por exemplo, atribua a uma propriedade do componente
                this.perfilModel = perfil;
            },
            (error) => {
                console.error('Erro ao obter informações do usuário', error);
            }
        );
    } else {
        console.error('userId não encontrado no localStorage');
    }
    
  }
  
  formatarAltura(altura: number | null | undefined): string {
    if (altura !== undefined && altura !== null) {
      return altura.toFixed(2);
    } else {
      return 'N/A'; // ou qualquer valor indicativo que você preferir
    }
  }
  
}
