import { Component, Input, OnInit } from '@angular/core';



@Component({
  selector: 'app-NavBar',
  templateUrl: './NavBar.component.html',
  styleUrls: ['./NavBar.components.css']
})
export class NavBarComponent implements OnInit {

  @Input() menuItems: { path: string; icon: string }[] = [];

  

  constructor() { }

  ngOnInit() {
  }

}
