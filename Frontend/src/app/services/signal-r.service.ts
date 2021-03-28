import { ChartModel } from '../interfaces/ChartModel';
import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Trade } from '../model/trade';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  public trades: Trade[];

  public data: ChartModel[];
  private hubConnection: signalR.HubConnection;

  constructor() { }


  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('https://localhost:5001/chart')
                            .build();

    this.hubConnection
    .start()
    .then(()=> console.log('Connection started'))
    .catch(err=> console.log('Error while starting connection: ' + err))
  }

  public addTransferChartDataListener = () => {
    this.hubConnection.on('transferchartdata', (data)=> {
      this.data = data;
      //this.trades = data;
      console.log(data);
    })
  }

  public addTransferChartDataListenerToTrade = () => {
    this.hubConnection.on('transferchartdata', (data)=> {
      this.trades = data;
    })
  }

}
