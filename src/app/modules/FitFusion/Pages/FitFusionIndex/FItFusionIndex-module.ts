import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { CardComponent } from "../../components/Card/Card.component";
import { FitFusionIndexComponent } from "./FitFusionIndex.component";
import { RouterModule } from "@angular/router";
import { CardPerfilComponent } from "../../components/CardPerfil/CardPerfil.component";
import { CardNoticiasComponent } from "../../components/CardNoticias/CardNoticias.component";
import { FitFusionServicesService } from "../../services/FitFusionServices.service";
import { NavBarComponent } from "../../components/NavBar/NavBar.component";



@NgModule({
    declarations: [CardComponent,FitFusionIndexComponent,CardPerfilComponent, CardNoticiasComponent,NavBarComponent],
    imports: [RouterModule],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    providers:[FitFusionServicesService]
})

export class FitFusionIndexModule{}