import { User } from './../model/user';
import { UserService } from './../services/user.service';
import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public user;
  public returnUrl: string;
  public message: string;
  usuarioAutenticado: boolean;
  public activate_spinner: boolean;

  constructor(private router: Router, private activatedRouter: ActivatedRoute, private userService : UserService){
  }

  ngOnInit(): void {
    this.returnUrl = this.activatedRouter.snapshot.queryParams['returnUrl'];
    this.user = new User();
  }

  signIn() {
    this.activate_spinner = true;
    this.userService.verifyUser(this.user)
    .subscribe(
      user_json => {

        this.userService.user = user_json;

        if(this.returnUrl == null) {
          this.router.navigate(['/']);
        } else {
          this.router.navigate([this.returnUrl]);
        }
      },
      err => {
        console.log(err);
        this.message = err.error;
        this.activate_spinner = false;
      }
    );
  }

 }


