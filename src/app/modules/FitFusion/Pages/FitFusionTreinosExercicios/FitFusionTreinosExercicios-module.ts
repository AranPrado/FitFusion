import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';
import { NavBarModule } from '../../components/NavBar/NavBar-module';
import {FitFusionTreinosExerciciosComponent } from './FitFusionTreinosExercicios.component';
import { CardExercicioComponent } from '../../components/CardExercicio/CardExercicio.component';

@NgModule({
  declarations: [FitFusionTreinosExerciciosComponent,CardExercicioComponent],
  imports: [RouterModule, NavBarModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [FitFusionServicesService]
})
export class FitFusionExerciciosModule {}
