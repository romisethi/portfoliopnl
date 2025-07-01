import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TradeService } from '../../../services/trade';
import { Trade } from '../trade.model';

@Component({
  selector: 'app-trade-form',
  templateUrl: './trade-form.html',
  styleUrl: './trade-form.scss',
  standalone: true,
  imports: []
})
export class TradeForm implements OnInit {
  tradeForm: FormGroup;
  isEdit = false;
  tradeId: string | null = null;
  loading = false;
  error: string | null = null;

  constructor(
    private fb: FormBuilder,
    private tradeService: TradeService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.tradeForm = this.fb.group({
      tradeDate: ['', Validators.required],
      instrument: ['', Validators.required],
      quantity: [0, [Validators.required, Validators.min(1)]],
      price: [0, [Validators.required, Validators.min(0)]],
      side: ['BUY', Validators.required],
      counterparty: ['', Validators.required],
      trader: ['', Validators.required],
      status: ['']
    });
  }

  ngOnInit() {
    this.tradeId = this.route.snapshot.paramMap.get('id');
    if (this.tradeId) {
      this.isEdit = true;
      this.loading = true;
      this.tradeService.getTrade(this.tradeId).subscribe({
        next: (trade) => {
          this.tradeForm.patchValue(trade);
          this.loading = false;
        },
        error: () => {
          this.error = 'Failed to load trade.';
          this.loading = false;
        }
      });
    }
  }

  onSubmit() {
    if (this.tradeForm.invalid) return;
    this.loading = true;
    const trade: Trade = this.tradeForm.value;
    if (this.isEdit && this.tradeId) {
      this.tradeService.updateTrade(this.tradeId, trade).subscribe({
        next: () => this.router.navigate(['/trades']),
        error: () => {
          this.error = 'Failed to update trade.';
          this.loading = false;
        }
      });
    } else {
      this.tradeService.addTrade(trade).subscribe({
        next: () => this.router.navigate(['/trades']),
        error: () => {
          this.error = 'Failed to add trade.';
          this.loading = false;
        }
      });
    }
  }

  cancel() {
    this.router.navigate(['/trades']);
  }
}
