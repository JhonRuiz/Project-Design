import { Injectable } from '@angular/core';
import { Http, Headers} from "@angular/http";
import 'rxjs/add/operator/map';
import { tokenNotExpired} from 'angular2-jwt';

let api = "/API/";


@Injectable()
export class AuthService {
  authToken: any;
  user: any;

  constructor(private http: Http) { }

  registerUser(user) {
    let headers = new Headers();
    this.loadToken();
    headers.append('Authorization', this.authToken);
    headers.append('Content-Type', 'application/json');
    return this.http.post(api + 'register', user,{headers: headers})
    .map(res => res.json());
  }

  authenticateUser(user) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http.post(api + 'authenticate', user,{headers: headers})
    .map(res => res.json());
  }


  getProfile() {
    let headers = new Headers();
    this.loadToken();
    headers.append('Authorization', this.authToken);
    headers.append('Content-Type', 'application/json');
    return this.http.get(api + 'profile', {headers: headers})
    .map(res => res.json());
  }

    storeUserData(token, user) {
      localStorage.setItem('id_token', token);
      localStorage.setItem('user', JSON.stringify(user));
      this.authToken = token;
      this.user = user;
    }

  getAllUsers() {
    let headers = new Headers();
    this.loadToken();
    headers.append('Authorization', this.authToken);
    headers.append('Content-Type', 'application/json');
    return this.http.get(api + 'getAllUsers', {headers: headers})
    .map(res => res.json());
  }

  getAllAssetBundles() {
    let headers = new Headers();
    this.loadToken();
    headers.append('Authorization', this.authToken);
    headers.append('Content-Type', 'application/json');
    return this.http.get(api + 'getAllAssetBundles', {headers: headers})
    .map(res => res.json());
  }

  deleteUser(id) {
    let headers = new Headers();
    this.loadToken();
    headers.append('Authorization', this.authToken);
    headers.append('Content-Type', 'application/json');
    return this.http.post(api + 'deleteUser', id, {headers: headers})
    .map(res => res.json());
  }

  removeBundle(id) {
    let headers = new Headers();
    this.loadToken();
    headers.append('Authorization', this.authToken);
    headers.append('Content-Type', 'application/json');
    return this.http.post(api + 'removeBundle', id, {headers: headers})
    .map(res => res.json());
  }

  disableAccount(id) {
    let headers = new Headers();
    this.loadToken();
    headers.append('Authorization', this.authToken);
    headers.append('Content-Type', 'application/json');
    return this.http.post(api + 'disableAccount', id, {headers: headers})
    .map(res => res.json());
  }

  enableAccount(id) {
    let headers = new Headers();
    this.loadToken();
    headers.append('Authorization', this.authToken);
    headers.append('Content-Type', 'application/json');
    return this.http.post(api + 'enableAccount', id, {headers: headers})
    .map(res => res.json());
  }

  disableAdmin(id) {
    let headers = new Headers();
    this.loadToken();
    headers.append('Authorization', this.authToken);
    headers.append('Content-Type', 'application/json');
    return this.http.post(api + 'disableAdmin', id, {headers: headers})
    .map(res => res.json());
  }

  enableAdmin(id) {
    let headers = new Headers();
    this.loadToken();
    headers.append('Authorization', this.authToken);
    headers.append('Content-Type', 'application/json');
    return this.http.post(api + 'enableAdmin', id, {headers: headers})
    .map(res => res.json());
  }

  uploadBundle(formData) {
    //console.log(formData);
    let headers = new Headers();
    this.loadToken();
    headers.append('Authorization', this.authToken);
    return this.http.post(api + 'uploadAsset', formData, {headers: headers})
    .map(res => res.json());
  }
  
  loadToken() {
    const token = localStorage.getItem('id_token');
    this.authToken = token;
  }

  loggedIn() {
    return tokenNotExpired('id_token');
  }

  logout() {
    this.authToken = null;
    this.user = null;
    localStorage.clear();
  }
}
