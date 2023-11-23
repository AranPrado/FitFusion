import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-CardTreinos',
  templateUrl: './CardTreinos.component.html',
  styleUrls: ['./CardTreinos.component.css']
})
export class CardTreinosComponent implements OnInit {

  @Input() router: string = '';

  constructor() { }

  ngOnInit() {
  }

}
