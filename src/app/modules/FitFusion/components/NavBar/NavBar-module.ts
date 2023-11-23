import { NgModule } from '@angular/core';
import { NavBarComponent } from './NavBar.component';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';


@NgModule({
  declarations:[NavBarComponent],
  exports: [NavBarComponent],
  imports: [RouterModule, CommonModule],
})
export class NavBarModule { }