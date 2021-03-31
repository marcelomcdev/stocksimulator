import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  isExpanded = false;

  constructor(private router: Router, private userService: UserService) {

  }

  collapse(){
    this.isExpanded = false;
  }

  toggle(){
    this.isExpanded = !this.isExpanded;
  }

  public userLogged(): boolean{
    return this.userService.user_authenticated();
  }

  logout(){
    this.userService.clean_session();
    this.router.navigate(['/']);
  }

}
