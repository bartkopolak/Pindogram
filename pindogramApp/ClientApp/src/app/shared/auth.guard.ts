import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { isArray } from 'util';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
      if (localStorage.getItem('currentUser')) {
        const authorities = route.data['authorities'];
        if (isArray(authorities)) {
          const localData = JSON.parse(localStorage.getItem('currentUser'));
          if (authorities.indexOf(localData.group) !== -1) {
            return true;
          } else {
            this.router.navigate(['']);
            return false;
          }
        }
          return true;
        }
        this.router.navigate(['login']);
        return false;
      }

    isAuthenticated(): boolean {
      return localStorage.getItem('currentUser') ? true : false;
    }

    hasAnyAuthority(): any {
      const userData = JSON.parse(localStorage.getItem('currentUser'));
      return userData;
    }

}
