import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component } from '@angular/core';

@Component({
    selector: 'app-login-modal',
    templateUrl: './login-modal.component.html'
})
export class LoginModalComponent {

  constructor(public activeModal: NgbActiveModal) {}

  cancel() {
    this.activeModal.dismiss('cancel');
  }

  login() {}

}
