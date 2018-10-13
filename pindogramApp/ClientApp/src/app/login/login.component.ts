import { RegisterModalComponent } from './register-modal/register-modal.component';
import { Component } from '@angular/core';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { LoginModalComponent } from './login-modal/login-modal.component';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: [
      'login.component.scss'
    ]
})
export class LoginComponent {

  constructor(private modalService: NgbModal) {}

  openLogin() {
    this.modalService.open(LoginModalComponent as Component);
  }

  openRegister() {
    this.modalService.open(RegisterModalComponent as Component);
  }

}
