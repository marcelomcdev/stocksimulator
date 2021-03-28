import { HttpClient } from '@angular/common/http';
import { SignalRService } from './services/signal-r.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Stock Simulator';

  constructor(public signalRService: SignalRService, private http: HttpClient) {}

  ngOnInit(){
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListener();
    this.startHttpRequest();
  }

  private startHttpRequest = () => {
    this.http.get('https://localhost:5001/chart/').subscribe(res => console.log(res));
  }

}
