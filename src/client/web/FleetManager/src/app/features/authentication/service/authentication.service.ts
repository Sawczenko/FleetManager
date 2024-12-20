import { Injectable } from '@angular/core';
import {jwtDecode} from 'jwt-decode';
import {Router} from '@angular/router';
import {Observable} from 'rxjs';
import {ApiService} from '../../../core/services/api/api.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private token: string = 'token';

  constructor(private router: Router, private apiService: ApiService) { }

  setToken(token: string): void {
    localStorage.setItem(this.token, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.token);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    return token !== null;
  }

  logout(): void {
    localStorage.removeItem(this.token);
    this.router.navigate(['/login']);
  }

  getUserId(): number | null {
    const token = this.getToken();
    if (token) {
      const decodedToken: any = jwtDecode(token);
      return decodedToken.userId as number || null;
    }
    return null;
  }
}
