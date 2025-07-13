import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoveAdGroupComponent } from './remove-ad-group.component';

describe('RemoveAdGroupComponent', () => {
  let component: RemoveAdGroupComponent;
  let fixture: ComponentFixture<RemoveAdGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RemoveAdGroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoveAdGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
