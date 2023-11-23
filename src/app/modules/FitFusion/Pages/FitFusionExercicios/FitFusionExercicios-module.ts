import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';
import { NavBarModule } from '../../components/NavBar/NavBar-module';
import { FitFusionExerciciosComponent } from './FitFusionExercicios.component';
import { CardTreinoExercicioComponent } from '../../components/CardTreinoExercicio/CardTreinoExercicio.component';

@NgModule({
  declarations: [FitFusionExerciciosComponent,CardTreinoExercicioComponent],
  imports: [RouterModule, NavBarModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [FitFusionServicesService]
})
export class FitFusionExerciciosModule {}
