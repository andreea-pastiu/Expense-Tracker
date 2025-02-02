import { Injectable } from '@angular/core';
import axios from 'axios';
import { Income } from '../models/income.model';

@Injectable({
  providedIn: 'root'
})
export class IncomeService {
  private apiUrl = 'https://localhost:7029/api/incomes';

  async getIncomes(): Promise<Income[]> {
    const token = localStorage.getItem('token');
    const response = await axios.get<Income[]>(this.apiUrl, {
      headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
  }

  async addIncome(income: Income): Promise<Income> {
    const token = localStorage.getItem('token');
    const response = await axios.post<Income>(this.apiUrl, income, {
      headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
  }

  async updateIncome(id: number, income: Income): Promise<void> {
    const token = localStorage.getItem('token');
    await axios.put(`${this.apiUrl}/${id}`, income, {
      headers: { Authorization: `Bearer ${token}` }
    });
  }

  async deleteIncome(id: number): Promise<void> {
    const token = localStorage.getItem('token');
    await axios.delete(`${this.apiUrl}/${id}`, {
      headers: { Authorization: `Bearer ${token}` }
    });
  }
}
