import { environment } from './../../environments/environment';
import { QuoteModel } from './../_interfaces/quotemodel.model';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';


@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  public data: QuoteModel[];

  private hubConnection: signalR.HubConnection;
  public startConnection = ()=> {
    this.hubConnection = new signalR.HubConnectionBuilder()
                              .withUrl(environment.SERVER_URL + '/tradehub')
                              .build();
    this.hubConnection
    .start()
    .then(()=> console.log('Connection started'))
    .catch(err=> console.log('Error while starting connection: ' + err))

  }

  public addTradeDataListener = () => {
    this.hubConnection.on('notifyQuotes', (data) => {
      this.data = data;
      console.log(data);
    })
  }

}
