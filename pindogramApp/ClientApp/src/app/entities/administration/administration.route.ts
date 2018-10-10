import { Route } from '@angular/router';
import { AdministrationComponent } from './administration.component';

export const ADMINISTRATION_ROUTE: Route = {
    path: 'administration',
    component: AdministrationComponent,
    data: {
        authorities: [],
        pageTitle: 'Administration page'
    }
};
