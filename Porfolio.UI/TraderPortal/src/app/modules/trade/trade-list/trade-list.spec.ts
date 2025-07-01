import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TradeList } from './trade-list';

describe('TradeList', () => {
  let component: TradeList;
  let fixture: ComponentFixture<TradeList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TradeList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TradeList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
