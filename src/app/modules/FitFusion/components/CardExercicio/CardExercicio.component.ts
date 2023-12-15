import { Component, Input, OnInit } from '@angular/core';
import { ExercicioModel, TreinoModel } from '../../models/Models.model';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';

@Component({
  selector: 'app-CardExercicio',
  templateUrl: './CardExercicio.component.html',
  styleUrls: ['./CardExercicio.component.css']
})
export class CardExercicioComponent implements OnInit {

  @Input() treino: TreinoModel | undefined;

  constructor(private fitFusionService: FitFusionServicesService) { }

  exercicios: ExercicioModel[] = [];

  ngOnInit() {
    this.exercicio();
  }

  exercicio() {
    if (this.treino) {
      this.fitFusionService.exercicioTreino(this.treino.treinoId).subscribe(
        (exercicios: ExercicioModel[]) => {
          this.exercicios = exercicios;
          console.log(exercicios);
        },
        (error) => {
          console.error('Erro ao obter exercícios do treino:', error);
        }
      );
    }
  }
}
