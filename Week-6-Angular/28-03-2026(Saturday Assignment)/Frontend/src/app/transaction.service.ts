import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Transaction {
  id?: number;
  date: string;
  description: string;
  type: number;
  amount: number;
  balance: string;
}

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  // 🔥 YOUR BACKEND PORT
  private apiUrl = 'http://localhost:5256/api/transactions';

  constructor(private http: HttpClient) {}

  getTransactions(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(this.apiUrl);
  }

  createTransaction(tx: Transaction) {
    return this.http.post(this.apiUrl, tx);
  }

  filterByDate(date: string): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.apiUrl}/filter?date=${date}`);
  }

  sortByAmount(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.apiUrl}/sort/amount`);
  }

  search(date?: string, sortBy?: string): Observable<Transaction[]> {
    let url = `${this.apiUrl}/search?`;

    if (date) url += `date=${date}&`;
    if (sortBy) url += `sortBy=${sortBy}`;

    return this.http.get<Transaction[]>(url);
  }
}