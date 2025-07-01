import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TradeForm } from './trade-form/trade-form';
import { TradeList } from './trade-list/trade-list';

const routes: Routes = [
  { path: '', component: TradeList },
  { path: 'new', component: TradeForm },
  { path: 'edit/:id', component: TradeForm }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TradeRoutingModule { }
