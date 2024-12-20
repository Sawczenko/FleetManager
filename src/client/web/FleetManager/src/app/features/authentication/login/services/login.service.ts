import { Injectable } from '@angular/core';
import {LoginRequest} from '../models/login-request';
import {catchError, map, Observable, throwError} from 'rxjs';
import {ValueResult} from '../../../../core/services/api/models/result';
import {LoginResponse} from '../models/login-response';
import {LoginError} from '../models/login-error';
import {ApiService} from '../../../../core/services/api/api.service';
import {HttpErrorResponse} from '@angular/common/http';
import {AuthenticationService} from '../../service/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private apiService: ApiService, private authService: AuthenticationService) { }

  public login(loginRequest: LoginRequest): Observable<LoginResponse> {
    return this.apiService.post<ValueResult<LoginResponse>>('/Account/login', loginRequest).pipe(
      map((result: ValueResult<LoginResponse>) => {
        this.handleSuccessfulLogin(result);
        return result.value;
      }),
      catchError((error: HttpErrorResponse) => {
        console.error('An error occurred:', error);

        const errorMessage =
          error.error?.message ||
          (error.status === 0 ? 'Network error, please try again.' : 'An unknown error occurred.');

        return throwError(() => new LoginError(errorMessage));
      })
    )
  }

  private handleSuccessfulLogin(result: ValueResult<LoginResponse>) {
   this.authService.setToken(result.value.token);
  }
}
