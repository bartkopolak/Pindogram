import { Route } from '@angular/router';
import { AdministrationComponent } from './administration.component';
import { AuthGuard } from '../../shared/auth.guard';

export const ADMINISTRATION_ROUTE: Route = {
    path: 'administration',
    component: AdministrationComponent,
    data: {
        authorities: ['ADMIN']
    },
    canActivate: [AuthGuard]
};
