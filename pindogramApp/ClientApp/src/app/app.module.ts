import { AuthGuard } from './shared/auth.guard';
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
import { MessageService } from './shared/message.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './shared/jwt.interceptor';
import { ErrorInterceptor } from './shared/error.interceptor';

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
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    AuthGuard,
    MessageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
