import { Component, OnInit } from '@angular/core';
import { TransactionService, Transaction } from '../transaction.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-record-table',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './record-table.html',
  styleUrl: './record-table.css'
})
export class RecordTableComponent implements OnInit {

  transactions: Transaction[] = [];
  selectedDate: string = '';

  newTransaction: Transaction = {
  date: '',
  description: '',
  type: 0,
  amount: 0,
  balance: ''
};

  constructor(private service: TransactionService) {}

  ngOnInit(): void {
    this.loadTransactions();
  }

  loadTransactions() {
    this.service.getTransactions().subscribe({
      next: (data) => this.transactions = data,
      error: (err) => console.error(err)
    });
  }

  // ✅ Filter button
  filter() {
    if (!this.selectedDate) return;

    this.service.filterByDate(this.selectedDate).subscribe({
      next: (data) => {
        console.log("Filtered data:", data);
        console.log("Sending date:", this.selectedDate);
        this.transactions = data;
      },
      error: (err) => console.error(err)
    });
  }

  // ✅ Sort button (Amount header)
  sortByAmount() {
    this.service.sortByAmount().subscribe({
      next: (data) => this.transactions = data,
      error: (err) => console.error(err)
    });
  }

  // Reset
  reset() {
    this.selectedDate = '';
    this.loadTransactions();
  }

  createTransaction() {
  this.service.createTransaction(this.newTransaction).subscribe({
    next: () => {
      alert("Transaction added!");

      this.loadTransactions(); // refresh table

      // reset form
      this.newTransaction = {
        date: '',
        description: '',
        type: 0,
        amount: 0,
        balance: ''
      };
    },
    error: (err) => console.error(err)
  });
}
}