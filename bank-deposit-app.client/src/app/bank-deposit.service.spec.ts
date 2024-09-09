import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { BankDepositService } from './bank-deposit.service';

describe('BankDepositService', () => {
  let service: BankDepositService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(BankDepositService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
