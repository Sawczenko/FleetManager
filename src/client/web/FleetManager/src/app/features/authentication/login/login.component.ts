import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {LoginRequest} from './models/login-request';
import {LoginService} from './services/login.service';
import {LoginResponse} from './models/login-response';
import {LoginError} from './models/login-error';

@Component({
  selector: 'app-login',
  standalone: false,

  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private router: Router, private loginService: LoginService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  get usernameControl() {
    return this.loginForm.get('username');
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const formData = this.loginForm.value;
      let loginRequest = new LoginRequest(formData.email, formData.password);

      this.loginService.login(loginRequest).subscribe({
        next: (response: LoginResponse) => this.handleLogin(response),
        error: (error: LoginError) => this.handlerLoginError(error)
      });

    } else {
      console.error('Form is invalid');
    }
  }

  private handlerLoginError(error: LoginError) {
    console.error(error);
  }

  private handleLogin(response: LoginResponse) {
    this.router.navigate(['dashboard']);
  }
}
