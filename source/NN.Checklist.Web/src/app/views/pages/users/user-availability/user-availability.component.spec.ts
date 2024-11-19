import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserAvailabilityComponent } from './user-availability.component';

describe('UserAvailabilityComponent', () => {
  let component: UserAvailabilityComponent;
  let fixture: ComponentFixture<UserAvailabilityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserAvailabilityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserAvailabilityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
