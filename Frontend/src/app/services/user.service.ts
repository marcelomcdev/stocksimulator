import { User } from '../model/user';

import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseURL: string;
  private _usuario: User;

  get usuario(): User {
    let usuario_json = sessionStorage.getItem('usuario-autenticado');
    this._usuario = JSON.parse(usuario_json);
    return this._usuario;
  }

  set usuario(usuario: User){
    sessionStorage.setItem('usuario-autenticado', JSON.stringify(usuario));
    this._usuario = usuario;
  }

  public usuario_autenticado(): boolean {
    return this._usuario != null && this._usuario.email != '' && this._usuario.password != '';
  }

  public limpar_sessao(){
    sessionStorage.setItem('usuario-autenticado','');
    this._usuario = null;
  }

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseURL = baseUrl;
  }

  public verifyUser(usuario: User) : Observable<User> {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    const body = {
      email: usuario.email,
      senha: usuario.password
    }

    return this.http.post<User>(`${this.baseURL}api/usuario/VerificarUsuario` , body, { headers })
  }

  public cadastrarUsuario(usuario: User) : Observable<User> {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    const body = {
      email: usuario.email,
      senha: usuario.password,
      nome: usuario.username,
      cpf: usuario.cpf
    }

    return this.http.post<User>(this.baseURL + 'api/auth', body, {headers});

  }

}
