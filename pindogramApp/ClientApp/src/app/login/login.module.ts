import { LoginModalComponent } from './login-modal/login-modal.component';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';
import { LOGIN_ROUTE } from './login.route';
import { SharedModule } from '../shared/shared.module';
import { RegisterModalComponent } from './register-modal/register-modal.component';
import { UserService } from '../shared/users/users.service';
import { AlertService } from '../shared/alert/alert.service';
@NgModule({
  imports: [
    RouterModule.forRoot([ LOGIN_ROUTE ]),
    SharedModule
  ],
  declarations: [
    LoginComponent,
    LoginModalComponent,
    RegisterModalComponent
  ],
  entryComponents: [
    LoginModalComponent,
    RegisterModalComponent
   ],
   providers: [
    UserService,
    AlertService
   ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class LoginModule {}
