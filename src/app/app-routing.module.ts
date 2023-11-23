import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FitFusionLoginComponent } from './modules/FitFusion/Pages/FitFusionLogin/FitFusionLogin.component';
import { FitFusionCadastroComponent } from './modules/FitFusion/Pages/FitFusionCadastro/FitFusionCadastro.component';
import { FitFusionIndexComponent } from './modules/FitFusion/Pages/FitFusionIndex/FitFusionIndex.component';
import { AuthGuard } from './modules/FitFusion/guards/auth.guard';
import { FitFusionTreinosComponent } from './modules/FitFusion/Pages/FitFusionTreinos/FitFusionTreinos.component';
import { FitFusionTreinosExerciciosComponent } from './modules/FitFusion/Pages/FitFusionTreinosExercicios/FitFusionTreinosExercicios.component';
import { FitFusionExerciciosComponent } from './modules/FitFusion/Pages/FitFusionExercicios/FitFusionExercicios.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'FitFusion',
    pathMatch: 'full'
  },
  {
    path: 'FitFusion',
    component: FitFusionLoginComponent
  },
  {
    path: 'registro',
    component: FitFusionCadastroComponent
  },
  {
    path: 'index',
    component: FitFusionIndexComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'treinos',
    component: FitFusionTreinosComponent
  },
  {
    path: 'treinos/exercicios',
    component: FitFusionTreinosExerciciosComponent
  },
  {
    path: 'exercicios',
    component: FitFusionExerciciosComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
