import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../../shared/authentication.service';

@Component({
  selector: 'app-login-modal',
  templateUrl: './login-modal.component.html'
})

export class LoginModalComponent implements OnInit {
    loginForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    error = '';

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private authenticationService: AuthenticationService,
        private activeModal: NgbActiveModal) {}

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });
        this.authenticationService.logout();
    }

    get f() { return this.loginForm.controls; }

    onSubmit() {
        this.submitted = true;

        if (this.loginForm.invalid) {
            return;
        }

        this.loading = true;
        this.authenticationService.login(this.f.username.value, this.f.password.value)
          .pipe(first())
          .subscribe(
            data => {
              this.activeModal.dismiss('cancel');
              this.router.navigate(['/']);
            },
            error => {
              this.error = error;
              this.loading = false;
            });
    }

    cancel() {
      this.activeModal.dismiss('cancel');
    }

}
