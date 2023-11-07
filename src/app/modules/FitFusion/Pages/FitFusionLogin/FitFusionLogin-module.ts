import { NgModule } from "@angular/core";
import { FitFusionLoginComponent } from "./FitFusionLogin.component";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";


@NgModule({
    declarations: [FitFusionLoginComponent],
    imports: [CommonModule, FormsModule,RouterModule]
})

export class FitFusionLoginModule{}