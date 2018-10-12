import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component } from '@angular/core';

@Component({
    selector: 'app-register-modal',
    templateUrl: './register-modal.component.html'
})
export class RegisterModalComponent {

  constructor(public activeModal: NgbActiveModal) {}

  cancel() {
    this.activeModal.dismiss('cancel');
  }

  register() {}

}
