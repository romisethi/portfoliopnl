import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'trades',
    pathMatch: 'full'
  },
  {
    path: 'trades',
    loadChildren: () => import('./modules/trade/trade.module').then(m => m.TradeModule)
  },
  {
    path: 'user',
    loadChildren: () => import('./modules/user/user.module').then(m => m.UserModule)
  },
  {
    path: '**',
    redirectTo: 'trades'
  }
];
