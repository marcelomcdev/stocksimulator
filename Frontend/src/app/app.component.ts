import { environment } from './../environments/environment';
import { HttpClient } from '@angular/common/http';
import { SignalRService } from './services/signalr.service';
import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Message, MessageService } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(public signalRService: SignalRService, private http: HttpClient) {}

  ngOnInit(){
    this.signalRService.startConnection();
    this.signalRService.addTradeDataListener();
    this.startHttpRequest();
  }

  private startHttpRequest = () => {
    this.http.get(environment.SERVER_URL + '/tradehub')
    .subscribe(res => {
      console.log(res);
    });
  }

  // title = 'Stock Simulator';

  // private hubConnection: HubConnection;
  // msgs: Message[] = [];

  // ngOnInit() {
  //   let builder = new HubConnectionBuilder();
  //   this.hubConnection = builder.withUrl('/api/notificationhub').build();
  //   this.hubConnection.start();
  //   this.hubConnection.on('SendMessage', (type: string, message: string) => {
  //     this.msgs.push({severity: type, summary: message})
  //   });

  // }

}
