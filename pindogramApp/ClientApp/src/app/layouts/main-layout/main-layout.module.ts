import { AdministrationModule } from './../../entities/administration/administration.module';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HomeModule } from '../../home/home.module';

@NgModule({
    imports: [
        HomeModule,
        AdministrationModule
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class MainLayoutModule {}
