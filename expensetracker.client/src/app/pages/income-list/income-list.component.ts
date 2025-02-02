import { Component, OnInit } from '@angular/core';
import { IncomeService } from '../../services/income.service';
import { Income } from '../../models/income.model';

@Component({
  selector: 'app-incomes',
  standalone: false,
  templateUrl: './income-list.component.html',
  styleUrls: ['./income-list.component.scss']
})
export class IncomeListComponent implements OnInit {
  incomes: Income[] = [];
  newIncome: Income = { id: 0, source: '', amount: 0, date: '', isEditing: false };
  isLoading = true;
  currentPage = 1;
  itemsPerPage = 5;

  constructor(private incomeService: IncomeService) { }

  async ngOnInit() {
    await this.loadIncomes();
  }

  async loadIncomes() {
    try {
      this.incomes = await this.incomeService.getIncomes();
    } catch (error) {
      console.error('Error loading incomes:', error);
    } finally {
      this.isLoading = false;
    }
  }

  async addIncome() {
    if (!this.newIncome.source || this.newIncome.amount <= 0 || !this.newIncome.date) return;

    const addedIncome = await this.incomeService.addIncome(this.newIncome);
    this.incomes.unshift(addedIncome);
    this.newIncome = { id: 0, source: '', amount: 0, date: '', isEditing: false };
  }

  toggleEdit(income: Income) {
    income.isEditing = !income.isEditing;
  }

  async saveIncome(income: Income) {
    await this.incomeService.updateIncome(income.id, income);
    this.toggleEdit(income);
  }

  async deleteIncome(id: number) {
    if (confirm('Are you sure you want to delete this income?')) {
      await this.incomeService.deleteIncome(id);
      this.incomes = this.incomes.filter(income => income.id !== id);
    }
  }

  get totalPages(): number[] {
    const pageCount = Math.ceil(this.incomes.length / this.itemsPerPage);
    return Array.from({ length: pageCount }, (_, i) => i + 1);
  }

  get paginatedIncomes() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.incomes.slice(startIndex, startIndex + this.itemsPerPage);
  }

  changePage(page: number) {
    this.currentPage = page;
  }
}
