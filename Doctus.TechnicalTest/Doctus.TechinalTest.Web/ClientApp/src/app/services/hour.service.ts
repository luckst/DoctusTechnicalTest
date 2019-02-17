import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Hour } from '../models/hour';

@Injectable()
export class HourService {
  constructor(private http: HttpClient
    , @Inject('BASE_URL') private baseUrl: string) {

  }

  getHours(activityId: number) {
    return this.http.get(this.baseUrl + `api/hours/hours?activityId=${activityId}`).toPromise()
      .then(result => {
        return result;
      });
  }

  create(date: string, time: number, activityId: number) {
    return this.http.post<any>(this.baseUrl + `api/hours/create`, { Date: date, Time: time, ActivityId: activityId }).toPromise()
      .then(result => {

        return result;
      });
  }
}
