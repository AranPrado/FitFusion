import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-Card',
  templateUrl: './Card.component.html',
  styleUrls: ['./Card.component.css']
})
export class CardComponent implements OnInit {

  @Input() titulo: string = 'Titulo';
  @Input() emoji: string = 'question';
  @Input() router: string = '';

  constructor() { }

  ngOnInit() {
  }

}
