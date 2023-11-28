import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FitFusionServicesService } from '../../services/FitFusionServices.service';
import { NavBarComponent } from '../../components/NavBar/NavBar.component';
import { FitFusionTreinosComponent } from './FitFusionTreinos.component';
import { CardTreinosComponent } from '../../components/CardTreinos/CardTreinos.component';
import { NavBarModule } from '../../components/NavBar/NavBar-module';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [FitFusionTreinosComponent, CardTreinosComponent],
  imports: [RouterModule, NavBarModule, CommonModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [FitFusionServicesService]
})
export class FitFusionTreinosModule {}
