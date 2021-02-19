import { TradeService } from './../../services/trade.service';
import { Trade } from './../../model/trade';

import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-trade',
  templateUrl: './trade.component.html',
  styleUrls: ['./trade.component.scss']
})
export class TradeComponent implements OnInit {

  public trades: Trade[];

  constructor(private tradeService: TradeService) {

    this.tradeService.getMostTradedOperations()
    .subscribe(
      trades => {
        this.trades = trades
      }
      ,
      e => {
        console.log(e.error);
      }
    )

  }

  ngOnInit() {
  }

open(){

}

}
