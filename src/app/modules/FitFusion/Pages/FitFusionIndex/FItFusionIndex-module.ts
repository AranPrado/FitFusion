import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { CardComponent } from "../../components/Card/Card.component";
import { FitFusionIndexComponent } from "./FitFusionIndex.component";
import { RouterModule } from "@angular/router";
import { CardPerfilComponent } from "../../components/CardPerfil/CardPerfil.component";
import { CardNoticiasComponent } from "../../components/CardNoticias/CardNoticias.component";
import { FitFusionServicesService } from "../../services/FitFusionServices.service";
import { NavBarComponent } from "../../components/NavBar/NavBar.component";
import { NavBarModule } from "../../components/NavBar/NavBar-module";




@NgModule({
    declarations: [CardComponent,FitFusionIndexComponent,CardPerfilComponent, CardNoticiasComponent],
    imports: [RouterModule,NavBarModule],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    providers:[FitFusionServicesService]
})

export class FitFusionIndexModule{}