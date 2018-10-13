import { AlertComponent } from './alert/alert.component';
import { JwtInterceptor } from './jwt.interceptor';
import { ErrorInterceptor } from './error.interceptor';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
@NgModule({
    imports: [
      NgbModule.forRoot(),
      FormsModule,
      ReactiveFormsModule,
      CommonModule,
      HttpClientModule
    ],
    declarations: [
      AlertComponent
    ],
    providers: [
      { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
      { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ],
    exports: [
      AlertComponent,
      NgbModule,
      FormsModule,
      ReactiveFormsModule,
      CommonModule
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]

})
export class SharedModule {}
