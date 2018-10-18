import { AuthGuard } from './../../shared/auth.guard';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: [
        'navbar.scss'
    ]
})
export class NavbarComponent {

  constructor(private authenticate: AuthGuard, private router: Router) {}

  isAuthenticated() {
    return this.authenticate.isAuthenticated();
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.router.navigate(['/login']);
}

}
