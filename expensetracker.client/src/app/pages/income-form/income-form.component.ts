import { Component, EventEmitter, Output } from '@angular/core';
import { IncomeService } from '../../services/income.service';
import { Income } from '../../models/income.model';

@Component({
  selector: 'app-income-form',
  standalone: false,
  templateUrl: './income-form.component.html',
  styleUrls: ['./income-form.component.scss']
})
export class IncomeFormComponent {
  income: Income = { id: 0, source: '', amount: 0, date: '' };
  isEditing = false;

  @Output() incomeAdded = new EventEmitter<void>();

  constructor(private incomeService: IncomeService) { }

  async submitIncome() {
    if (this.isEditing) {
      await this.incomeService.updateIncome(this.income.id, this.income);
    } else {
      await this.incomeService.addIncome(this.income);
    }

    this.resetForm();
    this.incomeAdded.emit();
  }

  editIncome(income: Income) {
    this.income = { ...income };
    this.isEditing = true;
  }

  resetForm() {
    this.income = { id: 0, source: '', amount: 0, date: '' };
    this.isEditing = false;
  }
}
