import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewAdGroupComponent } from './new-ad-group.component';

describe('NewAdGroupComponent', () => {
  let component: NewAdGroupComponent;
  let fixture: ComponentFixture<NewAdGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewAdGroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewAdGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
