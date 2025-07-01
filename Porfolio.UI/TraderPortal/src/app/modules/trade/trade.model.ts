export interface Trade {
  id: string;
  tradeDate: string; // ISO string
  instrument: string;
  quantity: number;
  price: number;
  side: 'BUY' | 'SELL';
  counterparty: string;
  trader: string;
  status?: string;
  // Add more fields as needed
}
