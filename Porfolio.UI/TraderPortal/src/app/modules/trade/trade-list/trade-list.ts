import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TradeService } from '../../../services/trade';
import { Trade } from '../trade.model';

@Component({
  selector: 'app-trade-list',
  templateUrl: './trade-list.html',
  styleUrl: './trade-list.scss',
  standalone: true,
  imports: []
})
export class TradeList implements OnInit {
  trades: Trade[] = [];
  loading = false;
  error: string | null = null;

  constructor(private tradeService: TradeService, private router: Router) {}

  ngOnInit() {
    this.fetchTrades();
  }

  fetchTrades() {
    this.loading = true;
    this.tradeService.getTrades().subscribe({
      next: (data) => {
        this.trades = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load trades.';
        this.loading = false;
      }
    });
  }

  addTrade() {
    this.router.navigate(['/trades/new']);
  }

  editTrade(id: string) {
    this.router.navigate(['/trades/edit', id]);
  }

  deleteTrade(id: string) {
    if (confirm('Are you sure you want to delete this trade?')) {
      this.tradeService.deleteTrade(id).subscribe({
        next: () => this.fetchTrades(),
        error: () => this.error = 'Failed to delete trade.'
      });
    }
  }
}
