import { Component, Input, OnInit } from '@angular/core';
import { TreinoModel } from '../../models/Models.model';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';

@Component({
  selector: 'app-CardTreinos',
  templateUrl: './CardTreinos.component.html',
  styleUrls: ['./CardTreinos.component.css']
})
export class CardTreinosComponent implements OnInit {

  @Input() router: string = '';
  @Input() idTreino: number = 0;

  constructor(private fitFusionService: FitFusionServicesService) { }

  treinos: TreinoModel[] = [];
  res:any;
  ngOnInit() {
    const userId = localStorage.getItem('userId');

    if (userId) {
      this.fitFusionService.treinosInformacoes().subscribe(
        (treinos: TreinoModel[]) => {
          this.treinos = treinos;
          
          console.log(this.treinos);
        },
        (error) => {
          console.error('Erro ao obter informações de treinos:', error);
        }
      );
    }

  }
}
