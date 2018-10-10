import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HomeComponent } from './';

@NgModule({
    declarations: [
      HomeComponent,
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class HomeModule {}
