import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';


@NgModule({
    imports: [
      FormsModule,
      CommonModule
    ],
    declarations: [],
    providers: [],
    entryComponents: [],
    exports: [
      FormsModule,
      CommonModule
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]

})
export class SharedModule {}
