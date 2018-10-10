import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';
import { LOGIN_ROUTE } from './login.route';

@NgModule({
    imports: [
        RouterModule.forRoot([ LOGIN_ROUTE ])
    ],
    declarations: [
        LoginComponent,
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class LoginModule {}
