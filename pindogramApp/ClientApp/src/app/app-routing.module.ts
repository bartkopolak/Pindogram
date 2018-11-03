import { navbarRoute } from './layouts/navbar/navbar.route';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { errorRoute } from './layouts/error/error.route';
import { mainLayoutRoute } from './layouts/main-layout/main-layout.route';

const LAYOUT_ROUTES = [
  navbarRoute,
  mainLayoutRoute,
  ...errorRoute,
  {path: '**', redirectTo: ''}
];

@NgModule({
    imports: [
        RouterModule.forRoot(LAYOUT_ROUTES)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule {}
