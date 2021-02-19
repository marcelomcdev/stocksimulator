import { Trade } from '../model/trade';
import { environment } from '../../environments/environment';
import { Injectable, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TradeService implements OnInit {
  private _baseURL: string;

  public trades: Trade[];

  constructor(private http: HttpClient)
  {
    this._baseURL = environment.SERVER_URL;
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
}
