import { AlertComponent } from './alert/alert.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
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
