import { Component, OnInit } from '@angular/core';
import { FitFusionTreinosModule } from './FItFusionTreinos-module';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';
import { TreinoModel } from '../../models/Models.model';


@Component({
  selector: 'app-FitFusionTreinos',
  templateUrl: './FitFusionTreinos.component.html',
  styleUrls: ['./FitFusionTreinos.component.css']
})
export class FitFusionTreinosComponent implements OnInit {

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
