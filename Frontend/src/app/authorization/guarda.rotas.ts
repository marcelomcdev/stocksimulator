import { UserService } from './../services/user.service';
import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class GuardaRotas implements CanActivate {
  constructor(private router: Router, private userService: UserService) {

  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    if(this.userService.user_authenticated()) {
      return true;
    }

    this.router.navigate(['/entrar'], { queryParams: { returnUrl: state.url} });
    // se usuario autenticado
    return false;

  }

}
