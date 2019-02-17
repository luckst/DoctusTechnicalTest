import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Observable, BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isVisible = true;
  isExpanded = false;
  constructor(private authenticationService: AuthenticationService) {
    if (!this.authenticationService.getCurrentUser()) {
      this.isVisible = true;
    }
  }

  ngOnInit(): void {
    if (this.authenticationService.getCurrentUser()) {
      this.isVisible = true;
    }
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
