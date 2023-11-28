import { Component, Input, OnInit } from '@angular/core';
import { TreinoModel } from '../../models/Models.model';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';

@Component({
  selector: 'app-CardExercicio',
  templateUrl: './CardExercicio.component.html',
  styleUrls: ['./CardExercicio.component.css']
})
export class CardExercicioComponent implements OnInit {
  @Input() router: string = '';
  @Input() idTreino: number = 0;
  treinos: TreinoModel[] = []; // Adicione esta linha

  constructor(private fitFusionService: FitFusionServicesService) { }

  ngOnInit() {
    const userId = localStorage.getItem('userId');

    if (userId) {
      this.fitFusionService.treinosInformacoes().subscribe(
        (treinos: TreinoModel[]) => {
          this.treinos = treinos; // Atualize a propriedade 'treinos' aqui
          console.log(this.treinos);
        },
        (error) => {
          console.error('Erro ao obter informações de treinos:', error);
        }
      );
    }
  }

}
