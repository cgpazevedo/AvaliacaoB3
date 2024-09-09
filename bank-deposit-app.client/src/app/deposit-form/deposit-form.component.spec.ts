import { DepositFormComponent } from './deposit-form.component';

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { BankDepositService } from '../bank-deposit.service';

describe('DepositFormComponent', () => {
  let component: DepositFormComponent;
  let fixture: ComponentFixture<DepositFormComponent>;
  let bankDepositService: jasmine.SpyObj<BankDepositService>;

  beforeEach(async () => {
    const depositServiceSpy = jasmine.createSpyObj('BankDepositService', ['postBankDeposit']);

    await TestBed.configureTestingModule({
      //declarations: [DepositFormComponent],
      imports: [FormsModule, HttpClientTestingModule, DepositFormComponent],
      providers: [{ provide: BankDepositService, useValue: depositServiceSpy }]
    }).compileComponents();

    fixture = TestBed.createComponent(DepositFormComponent);
    component = fixture.componentInstance;
    bankDepositService = TestBed.inject(BankDepositService) as jasmine.SpyObj<BankDepositService>;
  });

  it('should not submit if deposit or month are less than or equal to 0', () => {
    component.deposit = 0;
    component.month = 0;

    component.onSubmit();

    expect(component.errorMessage).toBe('Deposit and Month must be greater than zero.');
    expect(bankDepositService.postBankDeposit).not.toHaveBeenCalled();
  });

  it('should call the service and handle a successful response', () => {
    const mockResponse = { finalYield: 1000, finalTax: 200 };
    bankDepositService.postBankDeposit.and.returnValue(of(mockResponse));

    component.deposit = 500;
    component.month = 12;

    component.onSubmit();

    expect(bankDepositService.postBankDeposit).toHaveBeenCalledWith({ deposit: 500, month: 12 });
    expect(component.finalYield).toBe(1000);
    expect(component.finalTax).toBe(200);
    expect(component.errorMessage).toBe('');
  });

  it('should handle a failed response from the service', () => {
    bankDepositService.postBankDeposit.and.returnValue(throwError(() => new Error('Server error')));

    component.deposit = 500;
    component.month = 12;

    component.onSubmit();

    expect(bankDepositService.postBankDeposit).toHaveBeenCalled();
    expect(component.errorMessage).toBe('Failed to fetch the response from the server.');
    expect(component.finalYield).toBeUndefined();
    expect(component.finalTax).toBeUndefined();
  });
});
