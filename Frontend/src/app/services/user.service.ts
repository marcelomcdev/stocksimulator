import { environment } from './../../environments/environment';
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

  constructor(private http: HttpClient) {
    this.baseURL = environment.SERVER_URL;

  }

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
    return this._user != null && this._user.email != '' && this._user.senha != '';
  }

  public clean_session(){
    sessionStorage.setItem('user-authenticated','');
    this._user = null;
  }



  public verifyUser(user: User) : Observable<User> {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    const body = {
      username: user.email,
      password: user.senha
    }

    return this.http.post<User>(`${this.baseURL}api/auth/sign_in` , body, { headers })
  }

  public createUser(user: User) : Observable<User> {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    const body = {
      name: user.name,
      email: user.email,
      password: user.senha,
      confirmpassword: true,
      username: user.username,
      cpf: user.cpf,
    }

    return this.http.post<User>(this.baseURL + 'api/auth/sign_up', body, {headers});

  }

}
