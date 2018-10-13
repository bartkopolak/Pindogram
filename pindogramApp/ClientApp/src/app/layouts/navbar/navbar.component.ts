import { AuthGuard } from './../../shared/auth.guard';
import { Component } from '@angular/core';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: [
        'navbar.scss'
    ]
})
export class NavbarComponent {

  constructor(private authenticate: AuthGuard) {}

  isAuthenticated() {
    return this.authenticate.isAuthenticated();
  }

}
