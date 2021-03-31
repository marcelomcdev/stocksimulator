import { ModalConfig } from './../../interfaces/ModalConfig';
import { Component, Injectable, OnInit } from '@angular/core';
import { Trade } from 'src/app/model/trade';

@Component({
  selector: 'app-buy-trade',
  templateUrl: './buy-trade.component.html',
  styleUrls: ['./buy-trade.component.css']
})
@Injectable()
export class BuyTradeComponent implements OnInit {

  public currentTrade: Trade;
  public quantity: any;

  // public modalConfig: ModalConfig

  constructor() { }

  ngOnInit(): void {
    //this.modalConfig.modalTitle = 'Comprar Ativo'
  }

  calculate(total, quantity)
  {
    return (total * quantity).toFixed(2);
  }



}
