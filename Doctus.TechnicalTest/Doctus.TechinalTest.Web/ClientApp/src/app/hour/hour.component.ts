import { Component, OnInit } from '@angular/core';
import { User } from '../models/user';
import { Hour } from '../models/hour';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HourService } from '../services/hour.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-hour',
  templateUrl: './hour.component.html',
  styleUrls: ['./hour.component.css']
})
/** hour component*/
export class HourComponent implements OnInit {

  currentUser: User;
  hours: Array<Hour> = new Array<Hour>();

  hourForm: FormGroup;
  loading = false;
  submitted = false;
  message = "";
  showMessage = false;
  activityId = "";
  /** hour ctor */
  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private hourService: HourService,
    private authenticationService: AuthenticationService) {
    if (!this.authenticationService.getCurrentUser()) {
      this.router.navigate(['/login']);
    }
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  ngOnInit(): void {
    this.activityId = this.route.snapshot.paramMap.get("id")

    this.hourForm = this.formBuilder.group({
      date: ['', Validators.required],
      time: ['', Validators.required]
    });

    this.hourService.getHours(parseInt(this.activityId))
      .then(
        data => {
          if (data) {
            this.hours = data as Array<Hour>;
          }
        }).catch(error => {
          alert(error);
          this.loading = false;
        });
  }

  get f() { return this.hourForm.controls; }

  onSubmitHour() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.hourForm.invalid) {
      return;
    }
    this.loading = true;
    this.hourService.create(this.f.date.value, this.f.time.value, parseInt(this.activityId))
      .then(
        data => {
          if (data.messageType === 1) {
            this.hourService.getHours(parseInt(this.activityId))
              .then(
                data2 => {
                  if (data2) {
                    this.hours = data2 as Array<Hour>;
                    this.loading = false;
                  }
                }).catch(error => {
                  alert(error);
                  this.loading = false;
                });
          } else {
            this.loading = false;
            alert(data.message);
          }
          this.message = data.message;
        }).catch(error => {
          alert(error);
          this.loading = false;
        });
  }

}
