import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  firstName = '';
  lastName = '';
  email = '';
  password = '';
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) { }

  async register() {
    try {
      await this.authService.register(this.firstName, this.lastName, this.email, this.password);
      this.router.navigate(['/login']);
    } catch (error) {
      this.errorMessage = 'Registration failed';
    }
  }
}
