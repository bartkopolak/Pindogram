import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AdministrationComponent } from './administration.component';
@NgModule({
  declarations: [
      AdministrationComponent,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AdministrationModule {}