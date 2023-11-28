import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';
import { NavBarModule } from '../../components/NavBar/NavBar-module';
import {FitFusionTreinosExerciciosComponent } from './FitFusionTreinosExercicios.component';
import { CardExercicioComponent } from '../../components/CardExercicio/CardExercicio.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [FitFusionTreinosExerciciosComponent,CardExercicioComponent],
  imports: [RouterModule, NavBarModule,CommonModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [FitFusionServicesService]
})
export class FitFusionExerciciosModule {}
