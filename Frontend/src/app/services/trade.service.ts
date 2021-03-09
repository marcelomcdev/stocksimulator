import { WebSocketService } from './websocket.service';
import { Trade } from '../model/trade';
import { environment } from '../../environments/environment';
import { Injectable, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TradeService implements OnInit {
  private _baseURL: string;

  public trades: Trade[];

  constructor(private http: HttpClient, private wsService: WebSocketService)
  {
    this._baseURL = environment.SERVER_URL;
    this.wsService.connect();
  }

  ngOnInit(): void {
    this.trades = [];
  }

  get headers(): HttpHeaders {
    return new HttpHeaders().set('content-type', 'application/json');
  }

  public getMostTradedOperations() : Observable<Trade[]> {
    return this.http.get<Trade[]>(this._baseURL + 'api/operation');
  }


  // liveData$ = this.wsService.messages$.pipe(
  //   map(rows => rows.data),
  //   catchError(error => { throw error }),
  //   tap({
  //     error: error => console.log('[Live component] Error:', error),
  //     complete: () => console.log('[Live component] Connection Closed')
  //   }
  //   )
  // );

}
