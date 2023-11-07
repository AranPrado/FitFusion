import { NgModule } from "@angular/core";
import { FitFusionCadastroComponent } from "./FitFusionCadastro.component";
import { CommonModule } from "@angular/common";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";


@NgModule({
    declarations: [FitFusionCadastroComponent],
    imports: [CommonModule, BrowserModule, FormsModule]
})

export class FitFusionCadastroModule { }