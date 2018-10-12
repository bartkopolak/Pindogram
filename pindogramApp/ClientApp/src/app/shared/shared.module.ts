import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
    imports: [
      NgbModule.forRoot(),
      FormsModule,
      CommonModule
    ],
    declarations: [],
    providers: [],
    entryComponents: [],
    exports: [
      NgbModule,
      FormsModule,
      CommonModule
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]

})
export class SharedModule {}
