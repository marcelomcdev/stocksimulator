import { HttpClient } from '@angular/common/http';
import { SignalRService } from './../../services/signal-r.service';
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

  constructor(private tradeService: TradeService, public signalRService: SignalRService, private http: HttpClient) { }

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListenerToTrade();
    this.startListening();
    this.startHttpRequest();
  }

  private startListening = () => {
    this.http.get('https://localhost:5001/api/chart/listen')
    .subscribe(res => { console.log(res) });
  }

  private startHttpRequest = () => {
  this.http.get('https://localhost:5001/api/chart')
    .subscribe(res => {
       console.log(res);
    })
  }


}
