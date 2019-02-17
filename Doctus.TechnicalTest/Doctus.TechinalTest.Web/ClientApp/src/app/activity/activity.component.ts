import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ActivityService } from '../services/activity.service';
import { User } from '../models/user';
import { AuthenticationService } from '../services/authentication.service';
import { Activity } from '../models/activity';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.css']
})
/** activity component*/
export class ActivityComponent implements OnInit {
  currentUser: User;
  activities: Array<Activity> = new Array<Activity>();

  activityForm: FormGroup;
  loading = false;
  submitted = false;
  message = "";
  showMessage = false;
  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private activityService: ActivityService,
    private authenticationService: AuthenticationService) {
    if (!this.authenticationService.getCurrentUser()) {
      this.router.navigate(['/login']);
    }
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  ngOnInit(): void {
    this.activityForm = this.formBuilder.group({
      description: ['', Validators.required]
    });

    this.activityService.getActivities(this.currentUser.userId)
      .then(
        data => {
          if (data) {
            this.activities = data as Array<Activity>;
          }
        }).catch(error => {
          alert(error);
          this.loading = false;
        });
  }

  get f() { return this.activityForm.controls; }

  onSubmitActivity() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.activityForm.invalid) {
      return;
    }
    this.loading = true;
    this.activityService.create(this.f.description.value, this.currentUser.userId)
      .then(
        data => {
          if (data.messageType === 1) {
            this.router.navigate([`/hours/${data.activityId}`]);
          } else {
            this.loading = false;
            this.showMessage = true;
          }
          alert(data.message);
        }).catch(error => {
          alert(error);
          this.loading = false;
        });
  }
}
