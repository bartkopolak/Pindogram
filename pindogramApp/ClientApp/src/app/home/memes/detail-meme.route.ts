import { DetailMemeComponent } from './detail-meme.component';
import { Route } from '@angular/router';
import { AuthGuard } from '../../shared/auth.guard';

export const DETAIL_MEME_ROUTE: Route = {
    path: 'meme/:id',
    component: DetailMemeComponent,
    data: {
        authorities: ['ADMIN', 'USER']
    },
    canActivate: [AuthGuard]
};
