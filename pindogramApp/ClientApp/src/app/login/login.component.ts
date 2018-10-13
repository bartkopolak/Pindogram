import { Router } from '@angular/router';
import { RegisterModalComponent } from './register-modal/register-modal.component';
import { Component, OnInit } from '@angular/core';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { LoginModalComponent } from './login-modal/login-modal.component';
import { AuthGuard } from '../shared/auth.guard';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: [
      'login.component.scss'
    ]
})
export class LoginComponent implements OnInit {

  constructor(private modalService: NgbModal,
    private authenticate: AuthGuard,
    private router: Router) {}

  ngOnInit() {
    if (this.authenticate.isAuthenticated()) {
      this.router.navigate(['/']);
    }
  }

  openLogin() {
    this.modalService.open(LoginModalComponent as Component);
  }

  openRegister() {
    this.modalService.open(RegisterModalComponent as Component);
  }

}
