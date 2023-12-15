import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-CardCriarTreino',
  templateUrl: './CardCriarTreino.component.html',
  styleUrls: ['./CardCriarTreino.component.css']
})
export class CardCriarTreinoComponent implements OnInit {

  constructor() { }
  @Output() fecharCardEvent = new EventEmitter<void>();

  fecharCard() {
    this.fecharCardEvent.emit();
  }

  ngOnInit() {
  }

}
