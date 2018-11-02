import { DETAIL_MEME_ROUTE } from './../../home/memes/detail-meme.route';
import { Route } from '@angular/router';
import { MainLayoutComponent } from './main-layout.component';
import { HOME_ROUTE } from '../../home/home.route';
import { ADMINISTRATION_ROUTE } from './../../entities/administration/administration.route';
import { AuthGuard } from '../../shared/auth.guard';

export const mainLayoutRoute: Route = {
    path: '',
    component: MainLayoutComponent,
    children: [
      HOME_ROUTE,
      ADMINISTRATION_ROUTE,
      DETAIL_MEME_ROUTE
    ],
    canActivate: [AuthGuard]

};
