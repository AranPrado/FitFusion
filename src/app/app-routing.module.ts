import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FitFusionLoginComponent } from './modules/FitFusion/Pages/FitFusionLogin/FitFusionLogin.component';
import { FitFusionCadastroComponent } from './modules/FitFusion/Pages/FitFusionCadastro/FitFusionCadastro.component';
import { FitFusionIndexComponent } from './modules/FitFusion/Pages/FitFusionIndex/FitFusionIndex.component';
import { AuthGuard } from './modules/FitFusion/guards/auth.guard';

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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
