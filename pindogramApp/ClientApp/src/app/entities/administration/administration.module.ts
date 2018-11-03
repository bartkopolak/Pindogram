import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AdministrationComponent } from './administration.component';
import { SharedModule } from './../../shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxChartsModule } from '@swimlane/ngx-charts';

@NgModule({
  imports: [
    SharedModule,
    NgxChartsModule,
    BrowserAnimationsModule
  ],
  declarations: [
      AdministrationComponent,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AdministrationModule {}
