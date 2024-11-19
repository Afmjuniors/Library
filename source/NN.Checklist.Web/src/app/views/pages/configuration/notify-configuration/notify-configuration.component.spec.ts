import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NotifyConfigurationComponent } from './notify-configuration.component';

describe('NotifyConfigurationComponent', () => {
  let component: NotifyConfigurationComponent;
  let fixture: ComponentFixture<NotifyConfigurationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NotifyConfigurationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NotifyConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
