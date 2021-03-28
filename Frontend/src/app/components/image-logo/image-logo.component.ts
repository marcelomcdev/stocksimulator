import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-image-logo',
  templateUrl: './image-logo.component.html',
  styleUrls: ['./image-logo.component.scss']
})
export class ImageLogoComponent implements OnInit {

  public imagePath : string;
  public altName : string;

  constructor() {

    this.imagePath = './assets/stock-simulator-logo.png';
    this.altName = 'My logo';
  }



  ngOnInit() {
  }

}
