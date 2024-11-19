import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { getTime } from 'date-fns';
import { Logout } from '../../../../core/auth';
import { ParameterService } from '../../../../core/auth/_services';
import { AppState } from '../../../../core/reducers';

@Injectable({ providedIn: "root" })
export class IdleService {

  private timer: any;
  private timer2: any;
  private inactivityTime: number = 300;
  public timeout: number = 0;

  constructor(
    public store: Store<AppState>,
    public parameterService: ParameterService,
    public router: Router
  ) {}

  public startTimer() {
    this.loadPolicy();
  }

  loadPolicy() {
    this.parameterService.GetPolicyParameter().subscribe(
      (obj) => {
        if (obj && obj != null) {
          this.inactivityTime = obj.inactivityTimeLimit;
          this.restartTimer();
        } else {
          this.restartTimer();
        }
      },
      (error) => {}
    );
  }

  public restartTimer() {
    this.stopTimer();
    this.timeout = 0;
    this.startWatch();
  }

  public stopTimer() {
    clearTimeout(this.timer);
    this.timer = null;
    clearTimeout(this.timer2);
    this.timer2 = null;
  }

  public startWatch() {
    this.timer = setTimeout(() => {
      this.logout();
    }, this.inactivityTime * 1000);
    this.timer2 = setInterval(() => {
      this.timeout++;
    }, 1000);
  }

  public logout() {
    this.stopTimer();
    this.store.dispatch(new Logout(true));
    this.router.navigate(['/inicio']);
    window.location.reload();
  }
}
