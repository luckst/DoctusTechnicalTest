import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Activity } from '../models/activity';

@Injectable()
export class ActivityService {
  constructor(private http: HttpClient
    , @Inject('BASE_URL') private baseUrl: string) {

  }

  getActivities(userId: number) {
    return this.http.get(this.baseUrl + `api/activities/activities?userId=${userId}`).toPromise()
      .then(result => {
        return result;
      });
  }

  create(description: string, userId: number) {
    return this.http.post<any>(this.baseUrl + `api/activities/create`, { Description: description, UserId: userId }).toPromise()
      .then(result => {

        return result;
      });
  }
}
