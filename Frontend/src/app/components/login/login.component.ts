import { UserService } from './../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/model/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public usuario;
  public returnUrl: string;
  public mensagem: string;
  usuarioAutenticado: boolean;
  private ativar_spinner: boolean;

  // constructor(private router: Router, private activatedRouter: ActivatedRoute, private userService : UserService){
  // }
  //constructor() { }

  constructor(private router: Router, private activatedRouter: ActivatedRoute, private userService: UserService){
  }

  ngOnInit(): void {
    this.returnUrl = this.activatedRouter.snapshot.queryParams['returnUrl'];
    this.usuario = new User();
  }


  entrar() {
    this.ativar_spinner = true;
    this.userService.verifyUser(this.usuario)
    .subscribe(
      usuario_json => {
        // Essa linha será executada em caso de retorno sem erros.
        //let usuarioRetorno: Usuario;
        //usuarioRetorno = usuario_json;
        //sessionStorage.setItem('usuario-autenticado', '1');
        //sessionStorage.setItem('email-usuario', usuarioRetorno.email);

        this.userService.user  = usuario_json;

        if(this.returnUrl == null) {
          this.router.navigate(['/']);
        } else {
          this.router.navigate([this.returnUrl]);
        }
        //console.log(data);
      },
      err => {
        console.log(err);
        this.mensagem = err.error;
        this.ativar_spinner = false;
      }
    );
  }

}
