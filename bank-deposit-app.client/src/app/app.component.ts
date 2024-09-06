import { Injectable, Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DepositFormComponent } from './deposit-form/deposit-form.component';

@Injectable({
  providedIn: 'root'
})

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, DepositFormComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {

  constructor() { }

  title = 'bank-deposit-app';
}


