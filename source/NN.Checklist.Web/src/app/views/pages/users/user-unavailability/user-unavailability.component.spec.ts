import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserUnavailabilityComponent } from './user-unavailability.component';

describe('UserUnavailabilityComponent', () => {
  let component: UserUnavailabilityComponent;
  let fixture: ComponentFixture<UserUnavailabilityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserUnavailabilityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserUnavailabilityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
