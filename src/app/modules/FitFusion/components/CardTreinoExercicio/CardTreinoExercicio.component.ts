import { Component, Input, OnInit } from '@angular/core';
import { TreinoModel } from '../../models/Models.model';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';

@Component({
  selector: 'app-CardTreinoExercicio',
  templateUrl: './CardTreinoExercicio.component.html',
  styleUrls: ['./CardTreinoExercicio.component.css']
})
export class CardTreinoExercicioComponent implements OnInit {

 
  @Input() router: string = '';
  @Input() idTreino: number = 0;

  constructor(private fitFusionService: FitFusionServicesService) { }

  treinos: TreinoModel[] = [];
  ngOnInit() {
    const userId = localStorage.getItem('userId');

    if (userId) {
      this.fitFusionService.treinosInformacoes().subscribe(
        (treinos: TreinoModel[]) => {
          this.treinos = treinos;
          // console.log(this.treinos[0].exercicios[0].nome);
          // console.log(this.treinos);
        },
        (error) => {
          console.error('Erro ao obter informações de treinos:', error);
        }
      );
    }

  }

}
