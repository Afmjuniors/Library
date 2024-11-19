import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BatchNotificationComponent } from './batch-notification.component';

describe('BatchNotificationComponent', () => {
  let component: BatchNotificationComponent;
  let fixture: ComponentFixture<BatchNotificationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BatchNotificationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BatchNotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
