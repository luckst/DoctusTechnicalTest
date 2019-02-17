import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';

import { User } from '../models/user';


@Injectable()
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public getCurrentUser(): User {
    return this.currentUserSubject.value;
  }

  login(username: string, password: string) {
    return this.http.post<any>(this.baseUrl + `api/security/authenticate`, { username, password }).toPromise()
      .then(result => {
        if (result.messageType === 1) {
          let user = new User();
          user.lastName = result.lastName;
          user.name = result.name;
          user.password = result.password;
          user.userId = result.userId;
          user.userName = result.userName;

          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(result);
        }

        return result;
      });
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }
}
