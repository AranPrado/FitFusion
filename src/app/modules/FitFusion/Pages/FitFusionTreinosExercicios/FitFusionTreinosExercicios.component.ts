import { Component, OnInit } from '@angular/core';
import { FitFusionExerciciosModule } from './FitFusionTreinosExercicios-module';
import { TreinoModel } from '../../models/Models.model';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';


@Component({
  selector: 'app-FitFusionExercicios',
  templateUrl: './FitFusionTreinosExercicios.component.html',
  styleUrls: ['./FitFusionTreinosExercicios.component.css']
})
export class FitFusionTreinosExerciciosComponent implements OnInit {
  
  constructor(private fitFusionService: FitFusionServicesService) { }

  treinos: TreinoModel[] = [];
  ngOnInit() {
    const userId = localStorage.getItem('userId');

    if (userId) {
      this.fitFusionService.treinosInformacoes().subscribe(
        (treinos: TreinoModel[]) => {
          this.treinos = treinos;
          
        },
        (error) => {
          console.error('Erro ao obter informações de treinos:', error);
        }
      );
    }

  }

}
