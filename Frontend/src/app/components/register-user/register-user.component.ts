import { UserService } from './../../services/user.service';
import { User } from 'src/app/model/user';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.scss']
})
export class RegisterUserComponent implements OnInit {

  public usuario;
  public mensagem: string;
  constructor(private userService: UserService) { }

  ngOnInit() {
    this.usuario = new User();
  }

  public registerUser(){
    this.userService.createUser(this.usuario)
    .subscribe(
      userJson => { console.log(userJson);
        this.userService.user  = userJson;
       },
      err=> {
        console.log(err);
        if(err.error.errors["Password"] != null)
          this.mensagem = err.error.errors.Password.toString();
        else
          this.mensagem = err.error;
      }
    );
  }
}
