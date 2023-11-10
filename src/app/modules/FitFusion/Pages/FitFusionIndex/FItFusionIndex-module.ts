import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { CardComponent } from "../../components/Card/Card.component";
import { FitFusionIndexComponent } from "./FitFusionIndex.component";
import { RouterModule } from "@angular/router";
import { CardPerfilComponent } from "../../components/CardPerfil/CardPerfil.component";



@NgModule({
    declarations: [CardComponent,FitFusionIndexComponent,CardPerfilComponent],
    imports: [RouterModule],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})

export class FitFusionIndexModule{}