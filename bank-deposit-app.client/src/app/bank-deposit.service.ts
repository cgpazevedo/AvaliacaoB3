// src/app/bank-deposit.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface BankDepositDtoRequest {
  deposit: number;
  month: number;
}

interface BankDepositDtoResponse {
  finalYield: number;
  finalTax: number;
}

@Injectable({
  providedIn: 'root'
})

export class BankDepositService {

  private apiUrl = 'http://localhost:5177/Interest'; 
  constructor(private http: HttpClient) { }

  postBankDeposit(request: BankDepositDtoRequest): Observable<BankDepositDtoResponse> {

    return this.http.post<BankDepositDtoResponse>(this.apiUrl, request);

  }
}
