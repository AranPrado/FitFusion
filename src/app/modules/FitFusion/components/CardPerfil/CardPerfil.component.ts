import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-CardPerfil',
  templateUrl: './CardPerfil.component.html',
  styleUrls: ['./CardPerfil.component.css']
})
export class CardPerfilComponent implements OnInit {

  @Input() NomeUsuario: string = '';
  @Input() CargoUsuario: string = '';
  @Input() PesoUsuario: any = '';
  @Input() AlturaUsuario: any = '';
  @Input() IdadeUsuario: any = '';

  constructor() { }

  ngOnInit() {

  }

}
