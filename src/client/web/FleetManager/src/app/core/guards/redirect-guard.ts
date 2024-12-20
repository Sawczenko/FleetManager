import {CanActivate, Router} from '@angular/router';
import {AuthService} from '../../features/authentication/service/authentication.service';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RedirectGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    console.log('elo')
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['']); // Przekierowanie do /home dla zalogowanych
    } else {
      this.router.navigate(['/login']); // Przekierowanie do /login dla niezalogowanych
    }
    return false; // Zatrzymaj na tej trasie
  }
}
