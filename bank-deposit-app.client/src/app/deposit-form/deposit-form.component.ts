import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BankDepositService } from '../bank-deposit.service';


@Component({
  selector: 'app-deposit-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './deposit-form.component.html',
  styleUrl: './deposit-form.component.css'
})
export class DepositFormComponent {

  deposit: number = 0;
  month: number = 0;
  finalYield?: number;
  finalTax?: number;
  errorMessage: string = '';

  constructor(private bankDepositService: BankDepositService) { }

  onSubmit() {
    if (this.deposit <= 0 || this.month <= 0) {
      this.errorMessage = 'Deposit and Month must be greater than zero.';
      return;
    }

    this.bankDepositService.postBankDeposit({ deposit: this.deposit, month: this.month })
      .subscribe(
        (response) => {
          console.log('FinalYield ' + response.finalYield)
          console.log('FinalTax ' + response.finalTax)
          this.finalYield = response.finalYield;
          this.finalTax = response.finalTax;
          this.errorMessage = '';
        },
        (error) => {
          this.errorMessage = 'Failed to fetch the response from the server.';
          console.error(error);
        }
      );
  }
}

