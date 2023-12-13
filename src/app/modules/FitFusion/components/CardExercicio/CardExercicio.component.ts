import { Component, Input, OnInit } from '@angular/core';
import { ExercicioModel, TreinoModel } from '../../models/Models.model';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';

@Component({
  selector: 'app-CardExercicio',
  templateUrl: './CardExercicio.component.html',
  styleUrls: ['./CardExercicio.component.css']
})
export class CardExercicioComponent implements OnInit {


  constructor(private fitFusionService: FitFusionServicesService) { }


  exercicios: ExercicioModel[] = [];

  ngOnInit() {
    
    
  }

  // private carregarExercicios(){
  //   this.fitFusionService.exerciciosDoTreino().subscribe((exercicios:ExercicioModel[]) => {

  //   })
  // }

}
