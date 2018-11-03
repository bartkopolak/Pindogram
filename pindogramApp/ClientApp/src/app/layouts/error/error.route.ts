import { Routes } from '@angular/router';

import { ErrorComponent } from './error.component';

export const errorRoute: Routes = [
    {
        path: 'error',
        component: ErrorComponent,
        data: {
            authorities: ['ADMIN', 'USER']
        }
    },
    {
        path: 'accessdenied',
        component: ErrorComponent,
        data: {
            authorities: ['ADMIN', 'USER'],
            error403: true
        },
    }
];
