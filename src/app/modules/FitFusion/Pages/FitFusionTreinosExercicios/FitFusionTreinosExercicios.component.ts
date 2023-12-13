import { Component, Input, OnInit } from '@angular/core';
import { FitFusionExerciciosModule } from './FitFusionTreinosExercicios-module';
import { ExercicioModel, TreinoModel } from '../../models/Models.model';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';


@Component({
  selector: 'app-FitFusionExercicios',
  templateUrl: './FitFusionTreinosExercicios.component.html',
  styleUrls: ['./FitFusionTreinosExercicios.component.css']
})
export class FitFusionTreinosExerciciosComponent implements OnInit {


  constructor(private fitFusionService: FitFusionServicesService) { }

  ngOnInit() {
    
  }

  

}
