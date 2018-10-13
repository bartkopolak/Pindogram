import './vendor.ts';

import { AppComponent } from './app.component';
import { ErrorComponent } from './layouts/error/error.component';
import { NavbarComponent } from './layouts/navbar/navbar.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { LoginModule } from './login/login.module';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { MainLayoutModule } from './layouts/main-layout/main-layout.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    MainLayoutComponent,
    ErrorComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    LoginModule,
    MainLayoutModule,
    AppRoutingModule,
    SharedModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
