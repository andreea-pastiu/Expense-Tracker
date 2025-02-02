import { Injectable } from '@angular/core';
import axios from 'axios';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7029/api/auth';

  async login(email: string, password: string) {
    const response = await axios.post(`${this.apiUrl}/login`, { email, password });
    localStorage.setItem('token', response.data.token);
    return response.data;
  }

  async register(firstName: string, lastName: string, email: string, password: string) {
    return axios.post(`${this.apiUrl}/register`, { firstName, lastName, email, password });
  }

  logout() {
    localStorage.removeItem('token');
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}
