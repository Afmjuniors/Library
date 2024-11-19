import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserPhonesComponent } from './user-phones.component';

describe('UserPhonesComponent', () => {
  let component: UserPhonesComponent;
  let fixture: ComponentFixture<UserPhonesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserPhonesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserPhonesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
