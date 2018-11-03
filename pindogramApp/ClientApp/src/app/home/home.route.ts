import { AuthGuard } from './../shared/auth.guard';
import { Route } from '@angular/router';
import { HomeComponent } from './home.component';

export const HOME_ROUTE: Route = {
    path: '',
    component: HomeComponent,
    data: {
        authorities: ['ADMIN', 'USER']
    },
    canActivate: [AuthGuard]
};
