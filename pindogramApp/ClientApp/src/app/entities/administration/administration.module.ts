import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AdministrationComponent } from './administration.component';
import { SharedModule } from './../../shared/shared.module';
@NgModule({
  imports: [
    SharedModule
  ],
  declarations: [
      AdministrationComponent,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AdministrationModule {}
