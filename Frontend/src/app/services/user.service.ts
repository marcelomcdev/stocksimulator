import { User } from '../model/user';

import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseURL: string;
  private _user: User;

  get user(): User {
    let user_json = sessionStorage.getItem('user-authenticated');
    this._user = JSON.parse(user_json);
    return this._user;
  }

  set user(user: User){
    sessionStorage.setItem('user-authenticated', JSON.stringify(user));
    this._user = user;
  }

  public user_authenticated(): boolean {
    return this._user != null && this._user.email != '' && this._user.password != '';
  }

  public clean_session(){
    sessionStorage.setItem('user-authenticated','');
    this._user = null;
  }

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseURL = baseUrl;
  }

  public verifyUser(user: User) : Observable<User> {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    const body = {
      email: user.email,
      senha: user.password
    }

    return this.http.post<User>(`${this.baseURL}api/auth/sign_in` , body, { headers })
  }

  public createUser(user: User) : Observable<User> {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    const body = {
      email: user.email,
      senha: user.password,
      nome: user.username,
      cpf: user.cpf
    }

    return this.http.post<User>(this.baseURL + 'api/auth', body, {headers});

  }

}
