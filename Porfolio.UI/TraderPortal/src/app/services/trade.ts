import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Trade } from '../modules/trade/trade.model';

@Injectable({
  providedIn: 'root'
})
export class TradeService {
  private apiUrl = 'https://your-gateway-service-url/api/trades'; // TODO: Update with actual GatewayService URL

  constructor(private http: HttpClient) { }

  getTrades(): Observable<Trade[]> {
    return this.http.get<Trade[]>(this.apiUrl);
  }

  getTrade(id: string): Observable<Trade> {
    return this.http.get<Trade>(`${this.apiUrl}/${id}`);
  }

  addTrade(trade: Trade): Observable<Trade> {
    return this.http.post<Trade>(this.apiUrl, trade);
  }

  updateTrade(id: string, trade: Trade): Observable<Trade> {
    return this.http.put<Trade>(`${this.apiUrl}/${id}`, trade);
  }

  deleteTrade(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
